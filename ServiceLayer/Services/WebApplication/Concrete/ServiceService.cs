using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ServiceVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exception.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Service> _repository;
        private readonly IToastNotification _toast;
        private const string Section = "Service Section";

        public ServiceService(IMapper mapper, IUnitOfWork unitOfWork, IToastNotification toast)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Service>();
            _toast = toast;
        }




        public async Task<List<ServiceListVM>> GetAllListAsync()
        {
            var serviceListVM = await _repository.GetAllEntity().ProjectTo<ServiceListVM>
                (_mapper.ConfigurationProvider).ToListAsync();

            return serviceListVM;
        }

        public async Task AddServiceAsync(ServiceAddVM request)
        {
            var service = _mapper.Map<Service>(request);
            await _repository.AddEntityAsync(service);
            await _unitOfWork.CommitAsync();

            _toast.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _repository.GetEntityByIdAsync(id);
            _repository.DeleteEntity(service);
            await _unitOfWork.CommitAsync();

            _toast.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task<ServiceUpdateVM> GetServiceById(int id)
        {
            var service = await _repository.Where(x => x.Id == id).ProjectTo<ServiceUpdateVM>(_mapper.ConfigurationProvider).SingleAsync();
            return service;
        }

        public async Task UpdateServiceAsync(ServiceUpdateVM request)
        {
            var service = _mapper.Map<Service>(request);
            _repository.UpdateEntity(service);
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

        // UI Service Methods ...

        public async Task<List<ServiceVMForUI>> GetAllListForUIAsync()
		{
            var uiList = await _repository.GetAllEntity().ProjectTo<ServiceVMForUI>
                (_mapper.ConfigurationProvider).ToListAsync();

            return uiList;
        }
    }
}
