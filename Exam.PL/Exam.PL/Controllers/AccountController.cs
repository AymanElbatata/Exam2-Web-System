using Exam.BLL.Interfaces;
using Exam.BLL.Repositories;
using Exam.PL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #region Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(model.Email);

                if (user is null)
                {
                    ModelState.AddModelError("", "Invalid Email");
                    return View(model);
                }

                var password = await unitOfWork.UserManager.CheckPasswordAsync(user, model.Password);

                if (password)
                {
                    var result = await unitOfWork.SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var isAdmin = await unitOfWork.UserManager.IsInRoleAsync(user, "Admin");
                        if (isAdmin)
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Password");
                }
            }
            return View(model);
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await unitOfWork.SignInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        #endregion


    }
}
