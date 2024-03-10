using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.ContactVM;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exception.WebApplication;
using ServiceLayer.Messages.WebApplication;
using ServiceLayer.Services.WebApplication.Abstract;

namespace ServiceLayer.Services.WebApplication.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepositories<Contact> _repository;
        private readonly IToastNotification _toast;
        private const string Section = "Contact Section";

        public ContactService(IMapper mapper, IUnitOfWork unitOfWork, IToastNotification toast)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Contact>();
            _toast = toast;
        }




        public async Task<List<ContactListVM>> GetAllListAsync()
        {
            var contactListVM = await _repository.GetAllEntity().ProjectTo<ContactListVM>
                (_mapper.ConfigurationProvider).ToListAsync();

            return contactListVM;
        }

        public async Task AddContactAsync(ContactAddVM request)
        {
            var contact = _mapper.Map<Contact>(request);
            await _repository.AddEntityAsync(contact);
            await _unitOfWork.CommitAsync();

            _toast.AddSuccessToastMessage(NotificationMessagesWebApplication.AddMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _repository.GetEntityByIdAsync(id);
            _repository.DeleteEntity(contact);
            await _unitOfWork.CommitAsync();

            _toast.AddWarningToastMessage(NotificationMessagesWebApplication.DeleteMessages(Section), new ToastrOptions
            {
                Title = NotificationMessagesWebApplication.SuccessedTitle
            });
        }

        public async Task<ContactUpdateVM> GetContactById(int id)
        {
            var contact = await _repository.Where(x => x.Id == id).ProjectTo<ContactUpdateVM>(_mapper.ConfigurationProvider).SingleAsync();
            return contact;
        }

        public async Task UpdateContactAsync(ContactUpdateVM request)
        {
            var contact = _mapper.Map<Contact>(request);
            _repository.UpdateEntity(contact);
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

        // UI Side Methods ...

        public async Task<List<ContactVMForUI>> GetAllListForUIAsync()
        {
            var uiList = await _repository.GetAllEntity().ProjectTo<ContactVMForUI>
                (_mapper.ConfigurationProvider).ToListAsync();

            return uiList;
        }
    }
}
