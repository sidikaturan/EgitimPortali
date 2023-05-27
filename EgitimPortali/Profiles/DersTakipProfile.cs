using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.DersTakipleri;

namespace EgitimPortali.Profiles
{
    public class DersTakipProfile : Profile
    {
        public DersTakipProfile()
        {
            CreateMap<DersTakip,DersTakipDto>();
            CreateMap<DersTakip,DersTakipUpdateRequest>();
            CreateMap<DersTakipPostRequest,DersTakip>();
            CreateMap<DersTakipDto,DersTakip>();
        }
    }
}