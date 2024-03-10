using EntityLayer.WebApplication.ViewModels.PortfolioVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IPortfolioService
    {
        Task<List<PortfolioListVM>> GetAllListAsync();
        Task AddPortfolioAsync(PortfolioAddVM request);
        Task DeletePortfolioAsync(int id);
        Task<PortfolioUpdateVM> GetPortfolioById(int id);
        Task UpdatePortfolioAsync(PortfolioUpdateVM request);
        Task<List<PortfolioVMForUI>> GetAllListForUIAsync();

	}
}
