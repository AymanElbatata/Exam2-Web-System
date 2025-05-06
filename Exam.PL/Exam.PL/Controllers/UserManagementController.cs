using System.Security.Claims;
using AutoMapper;
using Exam.BLL.Interfaces;
using Exam.DAL.BaseEntity;
using Exam.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace Exam.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;

        public UserManagementController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            this.unitOfWork = unitOfWork;
            this.Mapper = Mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Manage Users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await unitOfWork.UserManager.Users.Where(a => a.IsDeleted == false)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.UserName,                  
                })
                .ToListAsync();

            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var usermodel = new UserViewModel();
                usermodel.Email = user.Email;
                usermodel.FirstName = user.FirstName;
                usermodel.LastName = user.LastName;
                usermodel.UserName = user.UserName;
                usermodel.Id = user.Id;
                var UserinRules = unitOfWork.UserManager.Users.FirstOrDefault(u => u.Email == user.Email);
                usermodel.Roles = (List<string>) unitOfWork.UserManager.GetRolesAsync(UserinRules).Result ?? new List<string>();
                userViewModels.Add(usermodel);
            }

            return Json(userViewModels);
        }

        // GET: Users/GetRoles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await unitOfWork.RoleManager.Roles
                .Select(r => new { Id = r.Id, Name = r.Name })
                .ToListAsync();

            var RolesModels = new List<RoleViewModel>();
            foreach (var user in roles)
            {
                var roleModel = new RoleViewModel();
                roleModel.Id = user.Id;
                roleModel.Name = user.Name;
                RolesModels.Add(roleModel);
            }

            return Json(RolesModels);
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NormalizedUserName = model.FirstName + "." + model.LastName
                };

                var result = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.SelectedRoles != null && model.SelectedRoles.Any())
                    {
                        await unitOfWork.UserManager.AddToRolesAsync(user, model.SelectedRoles);
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        // GET: Users/GetUser/{id}
        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserInputModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                SelectedRoles = (await unitOfWork.UserManager.GetRolesAsync(user)).ToList()
            };

            return Json(model);
        }

        // POST: Users/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromBody] UserInputModelUpdate model)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return Json(new { success = false, error = "User not found" });
                }

                user.Email = model.Email;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var result = await unitOfWork.UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var currentRoles = await unitOfWork.UserManager.GetRolesAsync(user);
                    await unitOfWork.UserManager.RemoveFromRolesAsync(user, currentRoles);

                    if (model.SelectedRoles != null && model.SelectedRoles.Any())
                    {
                        await unitOfWork.UserManager.AddToRolesAsync(user, model.SelectedRoles);
                    }

                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        // POST: Users/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsDeleted = true;
                var result = await unitOfWork.UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, error = "User not found" });
        }

        #endregion

    }
}
