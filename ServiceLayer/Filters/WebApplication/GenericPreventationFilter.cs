using CoreLayer.BaseEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RepositoryLayer.UnitOfWorks.Abstract;

namespace ServiceLayer.Filters.WebApplication
{
	public class GenericPreventationFilter<T> : IAsyncActionFilter where T : class, IBaseEntity, new()
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IToastNotification _toast;

		public GenericPreventationFilter(IToastNotification toast, IUnitOfWork unitOfWork)
		{
			_toast = toast;
			_unitOfWork = unitOfWork;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var entityList = await _unitOfWork.GetGenericRepository<T>().GetAllEntity().ToListAsync();
            var methodName = typeof(T).Name;
			
			if (entityList.Any())
            {
				_toast.AddErrorToastMessage($"You already have an {methodName} section. Delete it first and try again!", new ToastrOptions
				{
					Title = "Error!"
				});
				context.Result = new RedirectToActionResult($"Get{methodName}List", methodName, new { Area = ("Admin") });
				return;
            }
			await next.Invoke();
			return;
        }
	}
}
