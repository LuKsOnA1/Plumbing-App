using EntityLayer.WebApplication.ViewModels.TestimonalVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface ITestimonalService
    {
        Task<List<TestimonalListVM>> GetAllListAsync();
        Task AddTestimonalAsync(TestimonalAddVM request);
        Task DeleteTestimonalAsync(int id);
        Task<TestimonalUpdateVM> GetTestimonalById(int id);
        Task UpdateTestimonalAsync(TestimonalUpdateVM request);
        Task<List<TestimonalVMForUI>> GetAllListForUI();
    }
}
