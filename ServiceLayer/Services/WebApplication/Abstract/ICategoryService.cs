using EntityLayer.WebApplication.ViewModels.CategoryVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryListVM>> GetAllListAsync();
        Task AddCategoryAsync(CategoryAddVM request);
        Task DeleteCategoryAsync(int id);
        Task<CategoryUpdateVM> GetCategoryById(int id);
        Task UpdateCategoryAsync(CategoryUpdateVM request);
        Task<List<CategoryVMForUI>> GetAllListForUIAsync();

	}
}
