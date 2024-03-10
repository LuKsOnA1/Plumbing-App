using EntityLayer.WebApplication.Entities;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Filters.WebApplication;

namespace ServiceLayer.Extensions.WebApplication
{
	public static class WebApplicationExtensions
	{
		public static IServiceCollection LoadWebApplicationExtensions(this IServiceCollection services)
		{
			services.AddScoped(typeof(GenericPreventationFilter<About>));
			services.AddScoped(typeof(GenericNotFoundFilter<About>));

			services.AddScoped(typeof(GenericNotFoundFilter<Category>));

			services.AddScoped(typeof(GenericPreventationFilter<Contact>));
			services.AddScoped(typeof(GenericNotFoundFilter<Contact>));

			services.AddScoped(typeof(GenericPreventationFilter<HomePage>));
			services.AddScoped(typeof(GenericNotFoundFilter<HomePage>));

			services.AddScoped(typeof(GenericNotFoundFilter<Portfolio>));

			services.AddScoped(typeof(GenericNotFoundFilter<Service>));

			services.AddScoped(typeof(GenericNotFoundFilter<Team>));

			services.AddScoped(typeof(GenericNotFoundFilter<Testimonal>));

			return services;
		}
	}
}
