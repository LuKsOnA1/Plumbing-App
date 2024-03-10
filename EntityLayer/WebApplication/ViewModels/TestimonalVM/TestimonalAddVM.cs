﻿using Microsoft.AspNetCore.Http;

namespace EntityLayer.WebApplication.ViewModels.TestimonalVM
{
	public class TestimonalAddVM
	{
		public string Comment { get; set; } = null!;
		public string FullName { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string FileName { get; set; } = null!;
		public string FileType { get; set; } = null!;

		public IFormFile Photo { get; set; } = null!;
	}
}
