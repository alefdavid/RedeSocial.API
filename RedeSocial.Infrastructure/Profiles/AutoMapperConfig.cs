using AutoMapper;

namespace RedeSocial.Infrastructure.Profiles
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsuarioProfile());
                cfg.AddProfile(new PublicacaoProfile());
                cfg.AddProfile(new AmizadeProfile());
            });

            return config.CreateMapper();
        }
    }
}
