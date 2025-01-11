using AutoMapper;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Infrastructure.Profiles
{
    public class AmizadeProfile : Profile
    {
        public AmizadeProfile()
        {
            CreateMap<Amizade, AmizadeDTO>();
            CreateMap<AmizadeDTO, Amizade>();
        }
    }
}
