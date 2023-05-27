using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Iletisim;

namespace EgitimPortali.Profiles
{
    public class IletisimProfile : Profile
    {
        public IletisimProfile()
        {
            CreateMap<Iletisim, IletisimDto>();
            CreateMap<IletisimDto, Iletisim>();
            CreateMap<IletisimPostRequest, Iletisim>();
        }
    }
}


