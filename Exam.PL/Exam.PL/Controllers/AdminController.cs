using AutoMapper;
using Exam.BLL.Interfaces;
using Exam.BLL.Repositories;
using Exam.DAL.BaseEntity;
using Exam.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        public AdminController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            this.unitOfWork = unitOfWork;
            this.Mapper = Mapper;
        }
        public IActionResult Index()
        {
            return View();
        }


      

    }
}

