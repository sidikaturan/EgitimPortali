using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Konular;

namespace EgitimPortali.Profiles
{
    public class KonuProfile : Profile
    {
        public KonuProfile()
        {
            CreateMap<Konular, KonularDto>();
            CreateMap<KonularDto, Konular>();  
            CreateMap<KonularPostRequest, Konular>();  
            CreateMap<Konular, KonularUpdateRequest>();
        }
    }
}
