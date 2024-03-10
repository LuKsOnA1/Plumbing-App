using EntityLayer.WebApplication.ViewModels.ServiceVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IServiceService
    {
        Task<List<ServiceListVM>> GetAllListAsync();
        Task AddServiceAsync(ServiceAddVM request);
        Task DeleteServiceAsync(int id);
        Task<ServiceUpdateVM> GetServiceById(int id);
        Task UpdateServiceAsync(ServiceUpdateVM request);
        Task<List<ServiceVMForUI>> GetAllListForUIAsync();

	}
}
