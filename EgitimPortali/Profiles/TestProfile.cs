using AutoMapper;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Test;

namespace EgitimPortali.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestDto>();     
            CreateMap<TestDto, Test>();
            CreateMap<TestPostRequest, Test>();
            CreateMap<Test, TestUpdateRequest>();
        }
    }
}
