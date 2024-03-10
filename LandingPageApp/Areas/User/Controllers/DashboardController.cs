using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLandingPageApp.Areas.User.Controllers
{
	[Authorize(Roles = "Member,SuperAdmin")]
	[Area("User")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
