namespace ServiceLayer.Services.WebApplication.Abstract
{
	public interface IDashboardService
	{
		Task<int> GetAllServicesCountAsync();
		Task<int> GetAllTeamCountAsync();
		Task<int> GetAllTestimonalCountAsync();
		Task<int> GetAllCategoryCountAsync();
		Task<int> GetAllPortfolioCountAsync();
		int GetAllUserCountAsync();
	}
}
