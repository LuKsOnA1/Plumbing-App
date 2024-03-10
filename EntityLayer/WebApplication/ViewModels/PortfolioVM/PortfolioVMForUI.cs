using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.CategoryVM;

namespace EntityLayer.WebApplication.ViewModels.PortfolioVM
{
	public class PortfolioVMForUI
	{
		public string Title { get; set; } = null!;
		public string FileName { get; set; } = null!;

		public CategoryVMForUI Category { get; set; } = null!;
	}
}
