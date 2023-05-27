using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.AnaSayfa;

namespace EgitimPortali.Profiles
{
    public class AnasayfaProfile : Profile
    {
        public AnasayfaProfile()
        {
            CreateMap<AnaSayfa, AnasayfaDto>();
            CreateMap<AnasayfaDto, AnaSayfa>();
            CreateMap<AnaSayfa, AnaSayfaUpdateRequest>();
        }
    }
}
