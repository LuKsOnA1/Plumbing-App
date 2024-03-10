﻿using EntityLayer.WebApplication.ViewModels.CategoryVM;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.WebApplication.ViewModels.PortfolioVM
{
	public class PortfolioAddVM
	{
		public string Title { get; set; } = null!;
		public string FileName { get; set; } = null!;
		public string FileType { get; set; } = null!;

		public IFormFile Photo { get; set; } = null!;

		public int CategoryId { get; set; }
		public CategoryAddVM Category { get; set; } = null!;

		public IList<CategoryListVM> CategoryList { get; set; } = null!;
	}
}
