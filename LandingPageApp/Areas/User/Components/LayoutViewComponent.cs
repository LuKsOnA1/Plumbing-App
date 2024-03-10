using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebLandingPageApp.Areas.User.Components

{
    [Authorize]
    [Area("User")]
    public class LayoutViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public LayoutViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            // Here we are checking if we got username and if it is null we are giving one ...
            if(id == null)
            {
                id = UserClaimsPrincipal.Claims.FirstOrDefault(x => x.Type.Contains("identifier"))!.Value;
            }

            // Finding our user and checking: if FileName is null it will became "Default",  
            // if it is not null returning existing ...
            var user = await _userManager.FindByIdAsync(id);

            if(user!.FileName == null)
            {
                return View(new UserPictureVM { FileName = "Default" });
            }

            return View(new UserPictureVM { FileName = user.FileName });
        }
    }
}
