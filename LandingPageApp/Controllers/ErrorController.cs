﻿using CoreLayer.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Exception.WebApplication;

namespace WebLandingPageApp.Controllers
{
	public class ErrorController : Controller
	{
		private readonly ILogger<ErrorController> _logger;

		public ErrorController(ILogger<ErrorController> logger)
		{
			_logger = logger;
		}

		public IActionResult GeneralExceptions()
		{

			var exceptions = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;
			if (exceptions is ClientSideExceptions)
			{
				return View(new ErrorVM(exceptions.Message, 401));
			}

			if (exceptions.InnerException is SqlException sqlException && sqlException.Number == 547)
			{
				return View(new ErrorVM("You must delete all relevant data to move on.", 401));
			}

			_logger.LogError("The Error Message From System: ------" + exceptions.Message + "-------");
			return View(new ErrorVM("Server error. please speak to your Admin.", 500));
		}

		public IActionResult PageNotFound()
		{
			return View();
		}
	}
}
