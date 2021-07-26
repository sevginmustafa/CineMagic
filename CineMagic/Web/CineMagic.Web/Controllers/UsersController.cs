namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using CineMagic.Data.Models;
    using CineMagic.Web.Helpers;
    using CineMagic.Web.Infrastructure;
    using CineMagic.Web.ViewModels.InputModels.Users;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class UsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly Logger<ApplicationUser> logger;

        public UsersController(
            SignInManager<ApplicationUser> signInManager,
            Logger<ApplicationUser> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AjaxLoginInputModel loginInputModel, string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            var ajaxReturnObject = new AjaxReturnObject();

            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager
                    .PasswordSignInAsync(loginInputModel.Email, loginInputModel.Password, loginInputModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User logged in.");
                    ajaxReturnObject.Success = true;
                    ajaxReturnObject.Message = "logged-in";
                    ajaxReturnObject.Action = returnUrl;
                }

                if (result.IsLockedOut)
                {
                    this.logger.LogWarning("This account has been locked out, please try again later.");
                    return this.RedirectToPage("./Lockout");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "The email address or password supplied are incorrect. Please check your spelling and try again.");
                }

                // Login was unsuccessful, return model errors
                if (!ajaxReturnObject.Success)
                {
                    ajaxReturnObject.Message = ModelErrorsHelper.GetModelErorrs(this.ModelState);
                }

                return this.Json(ajaxReturnObject);
            }

            // If we got this far, something failed, redisplay form
            return this.View();
        }
    }
}
