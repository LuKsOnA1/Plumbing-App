using AutoMapper;
using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Helpers.Identity.ModelStateHelper;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Services.Identity.Abstract;
using System.Security.Claims;

namespace WebLandingPageApp.Controllers
{
    public class AuthenticationController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IValidator<SignUpVM> _signUpValidator;
		private readonly IValidator<LogInVM> _logInValidator;
		private readonly IValidator<ForgotPasswordVM> _forgotPasswordValidator;
		private readonly IValidator<ResetPasswordVM> _resetPasswordValidator;
		private readonly IMapper _iMapper;
		private readonly IAuthenticationMainService _authenticationService;
		private readonly IToastNotification _toast;

		public AuthenticationController(UserManager<AppUser> userManager, IValidator<SignUpVM> signUpValidator, IMapper iMapper,
			IValidator<LogInVM> logInValidator, SignInManager<AppUser> signInManager,
			IValidator<ForgotPasswordVM> forgotPasswordValidator,
			IValidator<ResetPasswordVM> resetPasswordValidator, IAuthenticationMainService authenticationService, IToastNotification toast)
		{
			_userManager = userManager;
			_signUpValidator = signUpValidator;
			_iMapper = iMapper;
			_logInValidator = logInValidator;
			_signInManager = signInManager;
			_forgotPasswordValidator = forgotPasswordValidator;
			_resetPasswordValidator = resetPasswordValidator;
			_authenticationService = authenticationService;
			_toast = toast;
		}

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpVM request)
		{
			// Validation ...
			var validation = await _signUpValidator.ValidateAsync(request);
			if (!validation.IsValid)
			{
				validation.AddToModelState(this.ModelState);
				return View();
			}

			var user = _iMapper.Map<AppUser>(request);

			// Creating user ...
			var userCreateResult = await _userManager.CreateAsync(user, request.Password);

			if (!userCreateResult.Succeeded)
			{
				ViewBag.Result = "Not Succeed";
				ModelState.AddModelErrorList(userCreateResult.Errors);
				return View();
			}

			// Assigning Role to new registered user
			var assignRoleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!assignRoleResult.Succeeded)
            {
				await _userManager.DeleteAsync(user);

				ViewBag.Result = "Not Succeed";
				ModelState.AddModelErrorList(assignRoleResult.Errors);
				return View();
			}

			var defaultClaim = new Claim("AdminObserverExpireDate", DateTime.Now.AddDays(-1).ToString());
			var addClaimResult = await _userManager.AddClaimAsync(user, defaultClaim);
			if (!addClaimResult.Succeeded)
			{
				await _userManager.RemoveFromRoleAsync(user, "Member");
				await _userManager.DeleteAsync(user);

				ViewBag.Result = "Not Succeed";
				ModelState.AddModelErrorList(addClaimResult.Errors);
				return View();
			}

			_toast.AddSuccessToastMessage(NotificationMessagesIdentity.SignUp(user.UserName!), new ToastrOptions
			{
				Title = NotificationMessagesIdentity.SuccessedTitle
            });

			return RedirectToAction("LogIn", "Authentication");
		}

		[HttpGet]
		public IActionResult LogIn(string? errorMessage)
		{
            if (errorMessage != null && errorMessage == IdentityMessages.SecurityStampError)
            {
				ViewBag.Result = "Not Succeed";
				ModelState.AddModelErrorList(new List<string> { errorMessage });
				return View();
			}

            if (errorMessage != null)
			{
				return Redirect("/Error/PageNotFound");
            }

            return View();
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LogInVM request, string? returnUrl = null)
		{
			// This Url is for authorization ...
			returnUrl = returnUrl ?? Url.Action("Index", "Dashboard", new { Area = "User" });

			// Validation ...
			var validation = await _logInValidator.ValidateAsync(request);
			if (!validation.IsValid)
			{
				validation.AddToModelState(this.ModelState);
				return View();
			}

			// Find user ...
			var hasUser = await _userManager.FindByEmailAsync(request.Email);
			if (hasUser == null)
			{
				ViewBag.Result = "Not Succeed";
				ModelState.AddModelErrorList(new List<string> { "Email or Password is Wrong!" });
				return View();
			}

			// Log In logic ...
			var logInResult = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);
			if (logInResult.Succeeded)
			{
				_toast.AddSuccessToastMessage(NotificationMessagesIdentity.LogInSuccess, new ToastrOptions
				{
					Title = NotificationMessagesIdentity.SuccessedTitle
                });
				return Redirect(returnUrl!);
			}

			// Locked Out ...
			if (logInResult.IsLockedOut)
			{
				ViewBag.Result = "LockedOut";
				ModelState.AddModelErrorList(new List<string> { "Your account is locked out for 60 seconds!" });
				return View();
			}

			ViewBag.Result = "Failed Attempt";
			ModelState.AddModelErrorList(new List<string>
			{ $"Email or Password is wrong! Failed attempt{await _userManager.GetAccessFailedCountAsync(hasUser)}" });

			return View();
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordVM request)
		{
			// Validation ...
			var validation = await _forgotPasswordValidator.ValidateAsync(request);
			if (!validation.IsValid)
			{
				validation.AddToModelState(this.ModelState);
				return View();
			}

			// Checking if user exists ...
			var hasUser = await _userManager.FindByEmailAsync(request.Email);
			if (hasUser == null)
			{
				ViewBag.Result = "UserDoesNotExist";
				ModelState.AddModelErrorList(new List<string> { "User Does Not Exist!" });
				return View();
			}

			_toast.AddSuccessToastMessage(NotificationMessagesIdentity.PasswordResetSuccess, new ToastrOptions
			{
				Title = NotificationMessagesIdentity.SuccessedTitle
			}); 

			// Logic For Send Email and Link ...
			await _authenticationService.CreateResetCredentialsAndSend(hasUser, HttpContext, Url, request);
			
			return RedirectToAction("LogIn", "Authentication");
		}

		[HttpGet]
		public IActionResult ResetPassword(string userId, string token, List<string> errors)
		{
			TempData["UserId"] = userId;
			TempData["Token"] = token;

			if (errors.Any())
			{
				ViewBag.Result = "Error";
				ModelState.AddModelErrorList(errors);
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordVM request)
		{
			var userId = TempData["UserId"];
			var token = TempData["Token"];

			// Checking if get 'user id' and 'token' ...
			if (userId == null || token == null)
			{
				_toast.AddErrorToastMessage(NotificationMessagesIdentity.TokenValidationError, new ToastrOptions
				{
					Title = NotificationMessagesIdentity.FailedTitle
                });
				return RedirectToAction("LogIn", "Authentication");
			}

			var validation = await _resetPasswordValidator.ValidateAsync(request);

			if (!validation.IsValid)
			{
				List<string> errors = validation.Errors.Select(x => x.ErrorMessage).ToList();
				return RedirectToAction("ResetPassword", "Authentication", new {userId, token, errors });
			}

			var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);
			if (hasUser == null)
			{
				_toast.AddErrorToastMessage(NotificationMessagesIdentity.UserError, new ToastrOptions
				{
					Title = NotificationMessagesIdentity.FailedTitle
                });
				return RedirectToAction("LogIn", "Authentication");
			}

			// Logic for reset password ...
			var resetPasswordResult = await _userManager.ResetPasswordAsync(hasUser!, token.ToString()!, request.Password);
			if (resetPasswordResult.Succeeded)
			{
				_toast.AddSuccessToastMessage(NotificationMessagesIdentity.PasswordChangeSuccess, new ToastrOptions
				{
					Title = NotificationMessagesIdentity.SuccessedTitle
				});
				return RedirectToAction("LogIn", "Authentication");
			}
			else
			{
				List<String> errors = resetPasswordResult.Errors.Select(x => x.Description).ToList();
				return RedirectToAction("ResetPassword", "Authentication", new { userId, token, errors });
			}
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
