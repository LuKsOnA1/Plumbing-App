using Microsoft.AspNetCore.Mvc;

namespace LandingPageApp.Controllers
{
	public class HomeController : Controller
	{
		

		public IActionResult Index()
		{
			return View();
		}


	}
}
