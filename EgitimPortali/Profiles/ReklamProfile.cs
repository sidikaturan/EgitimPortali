using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Reklamlar;

namespace EgitimPortali.Profiles
{
    public class ReklamProfile : Profile
    {
        public ReklamProfile()
        {
            CreateMap<Reklamlar, ReklamlarDto>();
            CreateMap<ReklamlarDto, Reklamlar>();
            CreateMap<ReklamlarPostRequest, Reklamlar>();
            CreateMap<Reklamlar, ReklamlarUpdateRequest>();
        }
    }
}
