using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.TestCevap;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EgitimPortali.Repository.TestCevaplari
{
    public class TestCevapRepository : ITestCevapRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TestCevapRepository(SqlServerDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }
        public int GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return Convert.ToInt16(result);
        }

     
        public ICollection<CozulenTestDto> KullaniciTestCevapListele()
        {
            var deger = _context.TestCevaps.Include(x => x.Test).Where(x => x.CreatedBy == GetMyName()).Select(x=> new {x.Test.Id ,x.Test.Name}).Distinct().ToList();    
            List<CozulenTestDto> tur = (from x in deger
                                        select new CozulenTestDto
                                        {
                                            Name = x.Name,
                                            Id = x.Id,
                                        }).ToList();
            return tur;
        }

        public bool TestCevapEkle(TestCevapPostRequest test)
        {
            var categoryMap = _mapper.Map<TestCevap>(test);
            _context.TestCevaps.Add(categoryMap);
            return Kaydet();
        }

        public TestCevap TestCevapGetir(int id)
        {
            return _context.TestCevaps.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool TestCevapKontrol(int id)
        {
            return _context.TestCevaps.Any(x => x.Id == id);
        }

        public ICollection<TestCevap> TestCevapListele()
        {
            return _context.TestCevaps.ToList();
        }

        public bool TestCevapSil(TestCevap test)
        {
            _context.TestCevaps.Remove(test);
            return Kaydet();
        }
      
        public ICollection<TestCevap> KullaniciTestCevaplariIdListele(int id)
        {
            return _context.TestCevaps.Where(x => x.TestId == id && x.CreatedBy == GetMyName()).ToList();
        }
    }
}
