using Exam.BLL.Interfaces;
using Exam.BLL.Repositories;
using Exam.DAL.BaseEntity;
using Exam.PL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Login(string? Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.Message = Message;
            }
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


        #region Register

        [AllowAnonymous]
        public IActionResult Register()
        {
            // Check if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to Home page (or Dashboard, etc.)
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (await unitOfWork.UserManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email is already registered");
                    return View(model);
                }

                // Create new user
                var user = new AppUser
                {
                    UserName = model.FirstName + "." + model.LastName + "-" + unitOfWork.MySPECIALGUID.GetUniqueKey(6),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                // Create the user
                var result = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await unitOfWork.UserManager.AddToRoleAsync(user, "User");
                    // Redirect to home page
                    return RedirectToAction("Login", "Account", new { Message = "Your account has been created successfully!" });
                }

                // Add errors if registration failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        #endregion

    }
}
