using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace WebLandingPageApp.Areas.Admin.Controllers
{
	[Authorize(Policy = "AdminObserver")]
	[Area("Admin")]
	public class DashboardController : Controller
	{
		private readonly IDashboardService _dashboardService;

		public DashboardController(IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}

		public async Task<IActionResult> Index()
		{
			ViewBag.Services = await _dashboardService.GetAllServicesCountAsync();
			ViewBag.Teams = await _dashboardService.GetAllTeamCountAsync();
			ViewBag.Testimonals = await _dashboardService.GetAllTestimonalCountAsync();
			ViewBag.Categories = await _dashboardService.GetAllCategoryCountAsync();
			ViewBag.Portfolios = await _dashboardService.GetAllPortfolioCountAsync();
			ViewBag.Users = _dashboardService.GetAllUserCountAsync();

			return View();
		}
	}
}
