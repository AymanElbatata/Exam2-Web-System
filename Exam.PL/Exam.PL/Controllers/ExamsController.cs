using System.Security.Claims;
using AutoMapper;
using Exam.BLL.Interfaces;
using Exam.DAL.Entities;
using Exam.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.PL.Controllers
{
    [Authorize(Roles = "User")]
    public class ExamsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        public ExamsController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            this.unitOfWork = unitOfWork;
            this.Mapper = Mapper;
        }
        public IActionResult Index()
        {
            var Model = unitOfWork.ExamTBLRepository.GetAll().Where(a=>a.IsDeleted == false && a.IsPublished == true);
            var MapperModel = Mapper.Map<IEnumerable<ExamTBLViewModel>>(Model);

            return View(MapperModel);
        }

        public async Task <IActionResult> StartExam(int ID = 0)
        {
            if (ID < 1) {             
                return BadRequest();
            }
            var Exam = unitOfWork.ExamTBLRepository.GetById(ID);
            if (Exam == null || Exam.IsDeleted == true)
                return BadRequest();

            var takeExamModel = new TakeExamModel();
            takeExamModel.ExamTBLId = Exam.ID;
            takeExamModel.Name = Exam.Name;
            takeExamModel.Body = Exam.Body;
            takeExamModel.DurationInnMinutes = Exam.DurationInMinutes;
            takeExamModel.SuccessRate = Exam.SuccessRate;
            takeExamModel.NumberofQuestions = Exam.NumberofQuestions;

            var Questions = unitOfWork.QuestionTBLRepository.GetAll().Where(a=>a.IsDeleted == false && a.ExamTBLId == Exam.ID).OrderBy(a=>a.ID);
            foreach (var item in Questions)
            {
                var Answers = unitOfWork.AnswerTBLRepository.GetAll().Where(a => a.QuestionTBLId == item.ID && a.IsDeleted == false);
                var questionViewModel = new QuestionViewModel();
                questionViewModel.Title = item.Title;
                questionViewModel.QuestionTBLId = item.ID;
                takeExamModel.Questions.Add(questionViewModel);
                foreach (var item2 in Answers)
                {
                    var answersViewModel = new AnswersViewModel();
                    answersViewModel.AnswerTBLId = item2.ID;
                    answersViewModel.Name = item2.Name;
                    questionViewModel.Answers.Add(answersViewModel);
                }
            }
            return View(takeExamModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartExam(ExamSubmissionModel model)
        {
            if (model == null)
                return BadRequest();
            var MissingAnswers = 0;
            foreach (var item in model.Questions)
            {
                if (item.SelectedAnswerId == null)
                {
                    ModelState.AddModelError("", "Please Answer All questions!");
                    return RedirectToAction("StartExam", new { ID = model.ExamTBLId});
                }
            }

            var UserExamDetailsTBLList = new List<UserExamDetailTBL>();

            var Exam = unitOfWork.ExamTBLRepository.GetById(model.ExamTBLId);
            int RightAnswersCounter = 0;

            var UserExamTBLRecord = new UserExamTBL();
            UserExamTBLRecord.ExamTBLId = model.ExamTBLId;
            UserExamTBLRecord.IsFinished = true;
            UserExamTBLRecord.CreatedUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int SAVEDUserExamTBLRecordID = unitOfWork.UserExamTBLRepository.AddAndReturnNewRowID(UserExamTBLRecord);

            foreach (var item in model.Questions)
            {
                if (item.SelectedAnswerId != null)
                {
                    if (AnswerIsRight(Convert.ToInt32(item.SelectedAnswerId)))
                        RightAnswersCounter++;
                }
                var UserExamDetailsTBLRecord = new UserExamDetailTBL();
                UserExamDetailsTBLRecord.UserExamTBLId = model.ExamTBLId;
                UserExamDetailsTBLRecord.QuestionTBLId = item.QuestionTBLId;
                if (item.SelectedAnswerId != null)
                {
                    UserExamDetailsTBLRecord.AnswerTBLId = item.SelectedAnswerId;
                }
                UserExamDetailsTBLRecord.CreatedUserID = UserExamTBLRecord.CreatedUserID;
                UserExamDetailsTBLRecord.UserExamTBLId = SAVEDUserExamTBLRecordID;
                unitOfWork.UserExamDetailTBLRepository.Add(UserExamDetailsTBLRecord);
            }

            UserExamTBLRecord.UserRate = (int)(((double)RightAnswersCounter / Exam.NumberofQuestions) * 100);
            UserExamTBLRecord.Result = UserExamTBLRecord.UserRate >= Exam.SuccessRate ? "Passed" : "Failed";
            unitOfWork.UserExamTBLRepository.Update(UserExamTBLRecord);
            // Process the submitted answers
            // Answers: Key = Question ID, Value = Selected Answer ID
            return RedirectToAction("Index","User");
        }

        private bool AnswerIsRight(int Answer)
        {
            return unitOfWork.AnswerTBLRepository.GetById(Answer).IsRight;
        }
    }
}
