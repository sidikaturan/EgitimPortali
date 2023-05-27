using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Hakkimizda;

namespace EgitimPortali.Profiles
{
    public class HakkimizdaProfile : Profile
    {
        public HakkimizdaProfile()
        {
            CreateMap<Hakkimizda, HakkimizdaDto>();
            CreateMap<HakkimizdaDto, Hakkimizda>();    
            CreateMap<Hakkimizda, HakkimizdaUpdateRequest>();
        }
    }
}
