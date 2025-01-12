using AutoMapper;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Profiles
{
    public class PublicacaoProfile : Profile
    {
        public PublicacaoProfile()
        {
            CreateMap<Publicacao, PublicacaoDTO>().ReverseMap();
        }        
    }
}
