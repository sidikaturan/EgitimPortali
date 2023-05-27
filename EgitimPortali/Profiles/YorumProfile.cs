using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Yorumlar;

namespace EgitimPortali.Profiles
{
    public class YorumProfile : Profile
    {
        public YorumProfile()
        {
            CreateMap<Yorumlar, YorumlarDto>();
            CreateMap<YorumlarDto, Yorumlar>();
            CreateMap<YorumlarPostRequest, Yorumlar>();
        }
    }
}