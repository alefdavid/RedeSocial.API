using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Repositories;

namespace RedeSocial.Infrastructure
{
    public static class DependencyInfrastrutureRegister
    {
        public static IServiceCollection RegisterInfrastrutureDependencies(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IAmizadeRepository, AmizadeRepository>();

            return services;
        }
    }
}
