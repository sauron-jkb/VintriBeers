
using System.Reflection;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace VintriBeers.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
		}
	}
}
