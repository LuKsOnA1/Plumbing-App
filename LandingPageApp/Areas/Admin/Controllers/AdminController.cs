using AutoMapper;
using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ServiceLayer.Messages.Identity;
using ServiceLayer.Services.Identity.Abstract;

namespace WebLandingPageApp.Areas.Admin.Controllers
{
	[Authorize(Policy = "AdminObserver")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IAuthenticationAdminService _admin;
        private readonly IToastNotification _toast;

		public AdminController(IToastNotification toast, IAuthenticationAdminService admin)
		{
			_toast = toast;
			_admin = admin;
		}

		public async Task<IActionResult> GetUserList()
        {
            var userListVM = await _admin.GetUserListAsync();

            return View(userListVM);
        }

        public async Task<IActionResult> ExtendClaim(string userName)
        {

            var renewClaim = await _admin.ExtendClaimAsync(userName);

            if (!renewClaim.Succeeded)
            {
                _toast.AddErrorToastMessage(NotificationMessagesIdentity.ExtandClaimFailed, new ToastrOptions
                {
                    Title = NotificationMessagesIdentity.FailedTitle
                });

                return RedirectToAction("GetUserList", "Admin", new {Area = "Admin"});
            }

			_toast.AddSuccessToastMessage(NotificationMessagesIdentity.ExtandClaimSuccess, new ToastrOptions
			{
				Title = NotificationMessagesIdentity.SuccessedTitle
			});

			return RedirectToAction("GetUserList", "Admin", new { Area = "Admin" });
		}
    }
}
