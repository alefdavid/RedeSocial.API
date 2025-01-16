using Microsoft.Extensions.DependencyInjection;
using RedeSocial.Domain;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Repositories;

namespace RedeSocial.Infrastructure
{
    public static class DependencyInfrastrutureRegister
    {
        public static IServiceCollection RegisterInfrastrutureDependencies(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAmizadeRepository, AmizadeRepository>();
            services.AddScoped<IPublicacaoRepository, PublicacaoRepository>();
            services.AddScoped<IAutorizacaoRepository, AutorizacaoRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();
            services.AddScoped<ICurtidaRepository, CurtidaRepository>();


            return services;
        }
    }
}
