﻿using EntityLayer.WebApplication.ViewModels.SocialMediaVM;
using EntityLayer.WebApplication.ViewModels.TeamVM;

namespace ServiceLayer.Services.WebApplication.Abstract
{
    public interface ITeamService
    {
        Task<List<TeamListVM>> GetAllListAsync();
        Task AddTeamAsync(TeamAddVM request);
        Task DeleteTeamAsync(int id);
        Task<TeamUpdateVM> GetTeamById(int id);
        Task UpdateTeamAsync(TeamUpdateVM request);
        Task<List<TeamVMForUI>> GetAllListForUIAsync();
    }
}
