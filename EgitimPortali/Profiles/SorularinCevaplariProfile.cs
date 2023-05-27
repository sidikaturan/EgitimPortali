using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.SorularinCevaplari;

namespace EgitimPortali.Profiles
{
    public class SorularinCevaplariProfile : Profile
    {
        public SorularinCevaplariProfile()
        {
            CreateMap<SorularinCevaplari, SoruCevapDto>();
            CreateMap<SorularinCevaplariPostRequest, SorularinCevaplari>();
            CreateMap<SoruCevapDto, SorularinCevaplari>();
        }
    }
}
