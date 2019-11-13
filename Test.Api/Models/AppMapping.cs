using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.DTO.DTOEntities;
using Test.Entities.Entities;

namespace Test.Api.Models
{
    public class AppMapping : Profile
    {
        public AppMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
        }
    }
}
