using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Sorular;
using EgitimPortali.Request.TestCevap;

namespace EgitimPortali.Profiles
{
    public class TestCevapProfile : Profile
    {
        public TestCevapProfile()
        {
            CreateMap<TestCevap, TestCevapDto>();
            CreateMap<TestCevapDto, TestCevap>();
            CreateMap<TestCevapPostRequest, TestCevap>();
        }
    }
}
