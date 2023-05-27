using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Sorular;

namespace EgitimPortali.Profiles
{
    public class SorularProfile : Profile
    {
        public SorularProfile()
        {
            CreateMap<Sorular, SorularDto>();
            CreateMap<SorularDto, Sorular>();
            CreateMap<SorularPostRequest, Sorular>();
        }
    }
}
