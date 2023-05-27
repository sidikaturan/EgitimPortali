using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.KullanicilarinRolleri;

namespace EgitimPortali.Profiles
{
    public class KullanicilarinRolleriProfile : Profile
    {
        public KullanicilarinRolleriProfile()
        {
            CreateMap<KullanicilarinRolleri, KullaniciRolDto>();
            CreateMap<KullaniciRolDto, KullanicilarinRolleri>();
            CreateMap<KullanicilarinRolleriPostRequest, KullanicilarinRolleri>();
            CreateMap<KullanicilarinRolleri, KullanicilarinRolleriUpdateRequest>();
        }
    }
}
