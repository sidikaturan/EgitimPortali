using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Roller;

namespace EgitimPortali.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<Roller, RolDto>();
            CreateMap<RolDto, Roller>();
            CreateMap<RollerPostRequest, Roller>();
            CreateMap<Roller, RollerUpdateRequest>();
        }
    }
}
