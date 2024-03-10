using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace WebLandingPageApp.Components
{
	public class ServiceViewComponent : ViewComponent
	{
		private readonly IServiceService _serviceService;

		public ServiceViewComponent(IServiceService serviceService)
		{
			_serviceService = serviceService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var serviceList = await _serviceService.GetAllListForUIAsync();

			return View(serviceList);
		}
	}
}
