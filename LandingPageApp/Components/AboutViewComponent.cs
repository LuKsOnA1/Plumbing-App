using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace WebLandingPageApp.Components
{
	public class AboutViewComponent : ViewComponent
	{
		private readonly IAboutService _aboutService;

		public AboutViewComponent(IAboutService aboutService)
		{
			_aboutService = aboutService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var aboutList = await _aboutService.GetAllListForUIAsync();
			return View(aboutList);
		}
	}
}
