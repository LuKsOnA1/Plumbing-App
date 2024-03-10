using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.CategoryVM;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exception.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Category> _repository;
        private readonly IToastNotification _toast;
        private const string Section = "Category Section";

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork, IToastNotification toast)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Category>();
            _toast = toast;
        }




        public async Task<List<CategoryListVM>> GetAllListAsync()
        {
            var categoryListVM = await _repository.GetAllEntity().ProjectTo<CategoryListVM>
                (_mapper.ConfigurationProvider).ToListAsync();

            return categoryListVM;
        }

        public async Task AddCategoryAsync(CategoryAddVM request)
        {
            var category = _mapper.Map<Category>(request);
            await _repository.AddEntityAsync(category);
            await _unitOfWork.CommitAsync();

            _toast.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetEntityByIdAsync(id);
            _repository.DeleteEntity(category);
            await _unitOfWork.CommitAsync();

            _toast.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task<CategoryUpdateVM> GetCategoryById(int id)
        {
            var category = await _repository.Where(x => x.Id == id).ProjectTo<CategoryUpdateVM>(_mapper.ConfigurationProvider).SingleAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(CategoryUpdateVM request)
        {
            var category = _mapper.Map<Category>(request);
            _repository.UpdateEntity(category);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				throw new ClientSideExceptions(ExceptionMessages.ConcurencyException);
			}

			_toast.AddInfoToastMessage(NotificationMessagesWebApplication.UpdateMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        // UI Side Methods ...

        public async Task<List<CategoryVMForUI>> GetAllListForUIAsync()
        {
            var uiList = await _repository.GetAllEntity().ProjectTo<CategoryVMForUI>
                (_mapper.ConfigurationProvider).ToListAsync();
            return uiList;
        }
    }
}
