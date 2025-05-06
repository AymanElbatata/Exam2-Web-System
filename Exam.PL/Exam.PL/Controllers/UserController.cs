using System.Security.Claims;
using AutoMapper;
using Exam.BLL.Interfaces;
using Exam.BLL.Repositories;
using Exam.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Exam.PL.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            this.unitOfWork = unitOfWork;
            this.Mapper = Mapper;
        }
        public IActionResult Index()
        {
            var Model = unitOfWork.UserExamTBLRepository.GetAllCustomized(
                filter: a => a.IsDeleted == false && a.IsFinished == true && a.CreatedUserID == User.FindFirstValue(ClaimTypes.NameIdentifier),
                includes: ue => ue.ExamTBL);

            var AccuratedModel = Mapper.Map<IEnumerable<UserExamTBLViewModel>>(Model);

            var MapperModel = Mapper.Map<IEnumerable<UserExamTBLViewModel>>(AccuratedModel);

            return View(MapperModel);
        }

    }
}
