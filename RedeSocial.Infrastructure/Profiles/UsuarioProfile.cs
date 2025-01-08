using AutoMapper;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();

            CreateMap<Usuario, ListarUsuarioDTO>().ReverseMap();
        }
    }
}
