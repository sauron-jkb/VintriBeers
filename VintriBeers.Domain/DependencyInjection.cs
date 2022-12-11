
using Microsoft.Extensions.DependencyInjection;

using VintriBeers.Domain.Data;

namespace VintriBeers.Domain
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDomain(this IServiceCollection services)
		{
			services.AddScoped<IUserRatingsContext, UserRatingsContext>();

			return services;
		}
	}
}
