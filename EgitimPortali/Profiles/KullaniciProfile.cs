using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Kullanicilar;

namespace EgitimPortali.Profiles
{
    public class KullaniciProfile : Profile
    {
        public KullaniciProfile()
        {
            CreateMap<Kullanicilar, KullaniciDto>();
            CreateMap<Kullanicilar, KullaniciReadDto>();
            CreateMap<KullaniciReadDto, Kullanicilar>();
            CreateMap<KullaniciDto, Kullanicilar>();
            CreateMap<Kullanicilar, KullanicilarUpdateRequest>();
            CreateMap<Kullanicilar, KullanicilarPostRequest>();
            CreateMap<KullanicilarPostRequest, Kullanicilar>();
            CreateMap<Kullanicilar, UserTokenReadDto>();
            CreateMap<RefreshToken, Kullanicilar>();
            CreateMap<Kullanicilar, RefreshToken>();
        }
    }
}
