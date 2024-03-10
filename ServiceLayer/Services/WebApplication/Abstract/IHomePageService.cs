using EntityLayer.WebApplication.ViewModels.ContactVM;
using EntityLayer.WebApplication.ViewModels.HomePageVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IHomePageService
    {
        Task<List<HomePageListVM>> GetAllListAsync();
        Task AddHomePageAsync(HomePageAddVM request);
        Task DeleteHomePageAsync(int id);
        Task<HomePageUpdateVM> GetHomePageById(int id);
        Task UpdateHomePageAsync(HomePageUpdateVM request);
        Task<List<HomePageVMForUI>> GetAllListForUIAsync();

	}
}
