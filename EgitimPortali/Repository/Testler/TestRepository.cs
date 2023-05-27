using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.Test;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EgitimPortali.Repository.Testler
{
    public class TestRepository : ITestRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TestRepository(SqlServerDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public ICollection<Test> KonuyaGoreTestListele(int id)
        {
            return _context.Tests.Where(x => x.KonularID == id).ToList();
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
        public bool KullaniciCozumKontrol(int id)
        {
            var deger = _context.TestCevaps.Where(x => x.TestId == id && x.CreatedBy == GetMyName()).FirstOrDefault();
            if (deger != null)
                return true;
            else
                return false;
        }

        public bool TestEkle(TestPostRequest test)
        {
            var categoryMap = _mapper.Map<Test>(test);
            _context.Tests.Add(categoryMap);
            return Kaydet();
        }

        public Test TestGetir(int id)
        {
            return _context.Tests.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool TestGuncelle(int id, TestUpdateRequest test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
                return false;
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
                return false;
            }

            Models.Test cases = _context.Tests.FirstOrDefault(u => u.Id == id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(id));
                return false;
            }

            if (test.Name != null) cases.Name = test.Name;
            if (test.KonularID != null) cases.KonularID = (int)test.KonularID;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, test);
            return Kaydet();
        }

        public bool TestKontrol(int id)
        {
            return _context.Tests.Any(x => x.Id == id);
        }

      
        public ICollection<Test> TestListele()
        {
            return _context.Tests.ToList();
        }

        public bool TestSil(Test test)
        {
            _context.Tests.Remove(test);
            return Kaydet();
        }
    }
}
