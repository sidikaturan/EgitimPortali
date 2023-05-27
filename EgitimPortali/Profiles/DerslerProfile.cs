using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Dersler;

namespace EgitimPortali.Profiles
{
    public class DerslerProfile : Profile
    {
        public DerslerProfile()
        {
            CreateMap<Dersler, DerslerDto>();
            CreateMap<DerslerDto, Dersler>();
            CreateMap<DerslerUpdateRequest, Dersler>();
            CreateMap<Dersler, DerslerUpdateRequest>();
        }
    }
}
