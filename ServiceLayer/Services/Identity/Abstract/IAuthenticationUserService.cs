﻿using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Services.Identity.Abstract
{
    public interface IAuthenticationUserService
    {
        Task<IdentityResult> UserEditAsync(UserEditVM request, AppUser user);
        Task<UserEditVM> FindUserAsync(HttpContext httpContext);
    }
}
