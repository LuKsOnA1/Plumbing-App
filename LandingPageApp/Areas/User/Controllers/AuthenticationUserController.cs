using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Helpers.Identity.ModelStateHelper;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Services.Identity.Abstract;

namespace WebLandingPageApp.Areas.User.Controllers
{
	[Authorize(Roles = "Member,SuperAdmin")]
	[Area("User")]
    public class AuthenticationUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IValidator<UserEditVM> _userEditValidator;
        private readonly IAuthenticationUserService _authenticationUserService;
        private readonly IToastNotification _toast;

		public AuthenticationUserController(UserManager<AppUser> userManager, IValidator<UserEditVM> userEditValidator, IAuthenticationUserService authenticationUserService, IToastNotification toast, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_userEditValidator = userEditValidator;
			_authenticationUserService = authenticationUserService;
			_toast = toast;
			_signInManager = signInManager;
		}

		[HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            var userEditVm = await _authenticationUserService.FindUserAsync(HttpContext);
            return View(userEditVm);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditVM request)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var validation = await _userEditValidator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return View();
            }
 

            var userEditResult = await _authenticationUserService.UserEditAsync(request, user!);
            if (!userEditResult.Succeeded)
            {
                ViewBag.Result = "FailedUserEdit";
                ModelState.AddModelErrorList(userEditResult.Errors);
                return View();
            }

            ViewBag.Id = user!.Id;

            _toast.AddInfoToastMessage(NotificationMessagesIdentity.UserEdit(user.UserName!), new ToastrOptions
            {
                Title = NotificationMessagesIdentity.SuccessedTitle
            });

            return RedirectToAction("Index", "Dashboard", new { Area = "User" });
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
