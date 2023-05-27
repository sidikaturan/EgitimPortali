using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Konular;
using EgitimPortali.Request.TestSoru;

namespace EgitimPortali.Profiles
{
    public class TestSoruProfile : Profile
    {
        public TestSoruProfile()
        {
            CreateMap<TestSoru, TestSoruDto>();
            CreateMap<TestSoruDto, TestSoru>();
            CreateMap<TestSoruPostRequest, TestSoru>();
            CreateMap<TestSoru, TestSoruUpdateRequest>();

        }
    }
}
