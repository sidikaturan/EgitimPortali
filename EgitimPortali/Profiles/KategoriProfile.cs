using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Kategoriler;

namespace EgitimPortali.Profiles
{
    public class KategoriProfile : Profile
    {
        public KategoriProfile()
        {
            CreateMap<Kategoriler, KategoriDto>();
            CreateMap<KategoriDto, Kategoriler>();          
            CreateMap<Kategoriler, KategoriUpdateRequest>();
        }
    }
}
