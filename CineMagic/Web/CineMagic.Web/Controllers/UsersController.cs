namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using CineMagic.Common;
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
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<UsersController> logger;
        private readonly IEmailSender emailSender;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<UsersController> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
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
                    ajaxReturnObject.Message = "Logged-in";
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
            }

            // Login was unsuccessful, return model errors
            if (!ajaxReturnObject.Success)
            {
                ajaxReturnObject.Message = ModelErrorsHelper.GetModelErorrs(this.ModelState);
            }

            return this.Json(ajaxReturnObject);
        }

        [HttpPost]
        public async Task<IActionResult> Register(AjaxRegisterInputModel registerInputModel, string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            var ajaxReturnObject = new AjaxReturnObject();

            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerInputModel.Username,
                    Email = registerInputModel.Email,
                };

                var result = await this.userManager.CreateAsync(user, registerInputModel.Password);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

                    ajaxReturnObject.Success = true;
                    ajaxReturnObject.Message = "Registered-in";
                    ajaxReturnObject.Action = returnUrl;

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        registerInputModel.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = registerInputModel.Email, returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            if (!ajaxReturnObject.Success)
            {
                ajaxReturnObject.Message = ModelErrorsHelper.GetModelErorrs(this.ModelState);
            }

            return this.Json(ajaxReturnObject);
        }
    }
}
