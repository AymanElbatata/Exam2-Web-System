using System.Net;
using AutoMapper;
using Exam.BLL.Interfaces;
using Exam.DAL.Entities;
using Exam.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageExamsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;

        public ManageExamsController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            this.unitOfWork = unitOfWork;
            this.Mapper = Mapper;
        }

        #region ManageExams
        public IActionResult Index()
        {
            var Model = unitOfWork.ExamTBLRepository.GetAll().Where(a => a.IsDeleted == false);
            var MapperModel = Mapper.Map<IEnumerable<ExamTBLViewModel>>(Model);

            return View(MapperModel);
        }

        public IActionResult GetExam(int id)
        {
            var exam = unitOfWork.ExamTBLRepository.GetById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Json(exam);
        }

        // POST: Exams/SaveExam
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveExam(ExamTBLViewModel exam)
        {
            if (ModelState.IsValid)
            {
                if (exam.ID == 0)
                {
                    // Add new exam
                    unitOfWork.ExamTBLRepository.Add(Mapper.Map<ExamTBL>(exam));
                }
                else
                {
                    // Update existing exam
                    unitOfWork.ExamTBLRepository.Update(Mapper.Map<ExamTBL>(exam));
                }
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteExam(int id)
        {
            var exam = unitOfWork.ExamTBLRepository.GetById(id);
            if (exam != null)
            {
                exam.IsDeleted = true;
                unitOfWork.ExamTBLRepository.Update(Mapper.Map<ExamTBL>(exam));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        #endregion

        #region ManageQuestions
        public IActionResult ManageQuestions(int examId)
        {
            var Model = unitOfWork.QuestionTBLRepository.GetAll().Where(a => a.IsDeleted == false && a.ExamTBLId == examId);
            var MapperModel = Mapper.Map<IEnumerable<QuestionTBLViewModel>>(Model);
            ViewBag.ExamID = examId;
            ViewBag.ExamName = unitOfWork.ExamTBLRepository.GetById(examId).Name;
            ViewBag.ExamBody = unitOfWork.ExamTBLRepository.GetById(examId).Body;
            return View(MapperModel);
        }

        [HttpGet]
        public IActionResult GetQuestions(int examId)
        {
            var questions = unitOfWork.QuestionTBLRepository.GetAll()
                .Where(a => a.IsDeleted == false && a.ExamTBLId == examId);
            var mappedQuestions = Mapper.Map<IEnumerable<QuestionTBLViewModel>>(questions);
            return Json(mappedQuestions);
        }

        public IActionResult GetQuestion(int id)
        {
            var exam = unitOfWork.QuestionTBLRepository.GetById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Json(exam);
        }

        // POST: Exams/SaveExam
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveQuestion(QuestionTBLViewModel exam)
        {
            if (ModelState.IsValid)
            {
                if (exam.ID == 0)
                {
                    // Add new exam
                    var NumberofQuestions = unitOfWork.ExamTBLRepository.GetById(exam.ExamTBLId).NumberofQuestions;
                    var CurrentQuestions = unitOfWork.QuestionTBLRepository.GetAll().Where(a=>a.IsDeleted == false && a.ExamTBLId == exam.ExamTBLId).Count();
                    if (CurrentQuestions <  NumberofQuestions)
                    unitOfWork.QuestionTBLRepository.Add(Mapper.Map<QuestionTBL>(exam));
                    else
                    {
                        //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        //return Json(new
                        //{success = false, error = "Sorry, Max required Questions Only: " });
                        return Json(new { success = false, error = "Sorry, Reached Maximum number of Questions" });

                    }
                }
                else
                {
                    // Update existing exam
                    unitOfWork.QuestionTBLRepository.Update(Mapper.Map<QuestionTBL>(exam));
                }
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQuestion(int id)
        {
            var exam = unitOfWork.QuestionTBLRepository.GetById(id);
            if (exam != null)
            {
                exam.IsDeleted = true;
                unitOfWork.QuestionTBLRepository.Update(Mapper.Map<QuestionTBL>(exam));
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        #endregion

        #region ManageAnswers
        public IActionResult ManageAnswers(int QuestionId)
        {
            var Model = unitOfWork.AnswerTBLRepository.GetAll().Where(a => a.IsDeleted == false && a.QuestionTBLId == QuestionId);
            var MapperModel = Mapper.Map<IEnumerable<AnswerTBLViewModel>>(Model);
            var Question = unitOfWork.QuestionTBLRepository.GetById(QuestionId);
            ViewBag.ExamName = unitOfWork.ExamTBLRepository.GetById(Question.ExamTBLId).Name;
            ViewBag.ExamBody = unitOfWork.ExamTBLRepository.GetById(Question.ExamTBLId).Body;
            ViewBag.QuestionTitle = Question.Title;
            ViewBag.QuestionId = Question.ID;
            ViewBag.ExamID = Question.ExamTBLId;
            return View(MapperModel);
        }

        [HttpGet]
        public IActionResult GetAnswer(int id)
        {
            var answer = unitOfWork.AnswerTBLRepository.GetById(id);
            if (answer == null || answer.IsDeleted)
            {
                return NotFound();
            }
            return Json(Mapper.Map<AnswerTBLViewModel>(answer));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAnswer(AnswerTBLViewModel answer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Mapper.Map<AnswerTBL>(answer);
                    if (unitOfWork.AnswerTBLRepository.GetAll().Where(a => a.IsRight == true && a.QuestionTBLId == answer.QuestionTBLId).Count() > 4)
                        return Json(new
                        {
                            success = false,
                            error = "Maximum 4 Answers!"
                        });

                    if (answer.ID == 0)
                    {
                        if (unitOfWork.AnswerTBLRepository.GetAll().Where(a=>a.IsRight == true && a.QuestionTBLId ==answer.QuestionTBLId && answer.IsRight == true).Any())
                            return Json(new
                            {
                                success = false,
                                error = "Every Question has only 1 Correct Answer!"
                            });
                        unitOfWork.AnswerTBLRepository.Add(entity);
                    }
                    else
                    {
                        if (unitOfWork.AnswerTBLRepository.GetAll().Where(a => a.IsRight == true && a.QuestionTBLId == answer.QuestionTBLId && answer.IsRight == true && a.ID != answer.ID).Any())
                            return Json(new
                            {
                                success = false,
                                error = "Every Question has only 1 Correct Answer!"
                            });
                        unitOfWork.AnswerTBLRepository.Update(entity);
                    }

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        success = false,
                        error = "An error occurred while saving: " + ex.Message
                    });
                }
            }

            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return Json(new
            {
                success = false,
                errors = errors,
                error = "Validation failed"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAnswer(int id)
        {
            var answer = unitOfWork.AnswerTBLRepository.GetById(id);
            if (answer != null)
            {
                answer.IsDeleted = true;
                unitOfWork.AnswerTBLRepository.Update(answer);
                return Json(new { success = true });
            }
            return Json(new { success = false, error = "Answer not found" });
        }
        #endregion
    }
}
