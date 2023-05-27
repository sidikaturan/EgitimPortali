using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.DersIcerikleri;

namespace EgitimPortali.Profiles
{
    public class DersIcerikleriProfile : Profile
    {
        public DersIcerikleriProfile()
        {
            CreateMap<DersIcerikleri, DersIcerikleriDto>();
            CreateMap<DersIcerikleriDto, DersIcerikleri>();
            CreateMap<DersIcerikleri, DersIcerikleriUpdateRequest>();
            CreateMap<DersIcerikleriPostRequest, DersIcerikleri>();
        }
    }
}
