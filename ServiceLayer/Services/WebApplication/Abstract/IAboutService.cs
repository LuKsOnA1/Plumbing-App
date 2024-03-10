﻿using EntityLayer.WebApplication.ViewModels.AboutVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface IAboutService
    {
        Task<List<AboutListVM>> GetAllListAsync();
        Task AddAboutAsync(AboutAddVM request);
        Task DeleteAboutAsync(int id);
        Task<AboutUpdateVM> GetAboutById(int id);
        Task UpdateAboutAsync(AboutUpdateVM request);
        Task<List<AboutVMForUI>> GetAllListForUIAsync();

	}
}
