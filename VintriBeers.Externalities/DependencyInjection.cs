
using Microsoft.Extensions.DependencyInjection;

using VintriBeers.Externalities.ExternalServices.PunkApi;

namespace VintriBeers.Externalities
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddExternalities(this IServiceCollection services)
		{
			services.AddScoped<IPunkApiService, PunkApiService>();

			return services;
		}
	}
}
