﻿using CoreLayer.BaseEntities;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Exception.WebApplication;

namespace ServiceLayer.Filters.WebApplication
{
	public class GenericNotFoundFilter<T> : IAsyncActionFilter where T : class, IBaseEntity, new()
	{
		private readonly IUnitOfWork _unitOfWork;

		public GenericNotFoundFilter(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var value = context.ActionArguments.FirstOrDefault().Value;
			if (value == null)
			{
				throw new ClientSideExceptions("Your input is invalid. Please try to use valid Id");
			}

			var id = (int)value!;
			var entity = await _unitOfWork.GetGenericRepository<T>().GetEntityByIdAsync(id);
            if (entity == null)
            {
				throw new ClientSideExceptions("The Id does not exist. Please try to use existing Id");
			}

            await next.Invoke();
			return;
		}
	}
}
