using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Application.Services;
using RedeSocial.Domain.Interfaces.Services;

namespace RedeSocial.Application
{
    public static class DependencyApplicationRegister
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioService, UsuarioService>();

            services.AddTransient<IAmizadeService, AmizadeService>();

            return services;
        }
    }
}
