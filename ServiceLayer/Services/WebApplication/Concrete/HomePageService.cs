using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.HomePageVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exception.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class HomePageService : IHomePageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<HomePage> _repository;
        private readonly IToastNotification _toast;
        private const string Section = "Home Page Section";

        public HomePageService(IMapper mapper, IUnitOfWork unitOfWork, IToastNotification toast)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<HomePage>();
            _toast = toast;
        }




        public async Task<List<HomePageListVM>> GetAllListAsync()
        {
            var homePageListVM = await _repository.GetAllEntity().ProjectTo<HomePageListVM>
                (_mapper.ConfigurationProvider).ToListAsync();

            return homePageListVM;
        }

        public async Task AddHomePageAsync(HomePageAddVM request)
        {
            var homePage = _mapper.Map<HomePage>(request);
            await _repository.AddEntityAsync(homePage);
            await _unitOfWork.CommitAsync();

            _toast.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task DeleteHomePageAsync(int id)
        {
            var homePage = await _repository.GetEntityByIdAsync(id);
            _repository.DeleteEntity(homePage);
            await _unitOfWork.CommitAsync();

            _toast.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task<HomePageUpdateVM> GetHomePageById(int id)
        {
            var homePage = await _repository.Where(x => x.Id == id).ProjectTo<HomePageUpdateVM>(_mapper.ConfigurationProvider).SingleAsync();
            return homePage;
        }

        public async Task UpdateHomePageAsync(HomePageUpdateVM request)
        {
            var homePage = _mapper.Map<HomePage>(request);
            _repository.UpdateEntity(homePage);
			var result = await _unitOfWork.CommitAsync();
			if (!result)
			{
				throw new ClientSideExceptions(ExceptionMessages.ConcurencyException);
			}

			_toast.AddWarningToastMessage(NotificationMessagesWebApplication.UpdateMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        // UI Service Methods ...

        public async Task<List<HomePageVMForUI>> GetAllListForUIAsync()
        {
            var uiList = await _repository.GetAllEntity().ProjectTo<HomePageVMForUI>
                (_mapper.ConfigurationProvider).ToListAsync();

            return uiList;
        }
    }
}
