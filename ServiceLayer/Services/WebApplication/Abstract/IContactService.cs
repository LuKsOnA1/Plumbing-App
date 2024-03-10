﻿using EntityLayer.WebApplication.ViewModels.CategoryVM;
using EntityLayer.WebApplication.ViewModels.ContactVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IContactService
    {
        Task<List<ContactListVM>> GetAllListAsync();
        Task AddContactAsync(ContactAddVM request);
        Task DeleteContactAsync(int id);
        Task<ContactUpdateVM> GetContactById(int id);
        Task UpdateContactAsync(ContactUpdateVM request);
        Task<List<ContactVMForUI>> GetAllListForUIAsync();

	}
}
