﻿using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.WebApplication.Abstract;

namespace WebLandingPageApp.Components
{
	public class ContactViewComponent : ViewComponent
	{
		private readonly IContactService _contactService;

		public ContactViewComponent(IContactService contactService)
		{
			_contactService = contactService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var contacList = await _contactService.GetAllListForUIAsync();

			return View(contacList);
		}
	}
}
