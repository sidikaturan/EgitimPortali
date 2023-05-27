using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.TestSoru;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.TestSorulari
{
    public class TestSoruRepository : ITestSoruRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;
        public TestSoruRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public ICollection<TestSoru> TesteGoreSoruListele(int id)
        {
            return _context.TestSorus.Where(x => x.TestId == id).OrderBy(x => x.SoruSirasi).ToList();
        }

        public bool TestSoruEkle(TestSoruPostRequest test)
        {
            var categoryMap = _mapper.Map<TestSoru>(test);
            _context.TestSorus.Add(categoryMap);
            return Kaydet();
        }

        public TestSoru TestSoruGetir(int id)
        {
            return _context.TestSorus.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool TestSoruGuncelle(int id, TestSoruUpdateRequest testsoru)
        {
            if (testsoru == null)
            {
                throw new ArgumentNullException(nameof(testsoru));
                return false;
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
                return false;
            }

            Models.TestSoru cases = _context.TestSorus.FirstOrDefault(u => u.Id == id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(id));
                return false;
            }

            if (testsoru.Soru != null) cases.Soru = testsoru.Soru;
            if (testsoru.CevapA != null) cases.CevapA = testsoru.CevapA;
            if (testsoru.CevapB != null) cases.CevapB = testsoru.CevapB;
            if (testsoru.CevapC != null) cases.CevapC = testsoru.CevapC;
            if (testsoru.CevapD != null) cases.CevapD = testsoru.CevapD;
            if (testsoru.CevapE != null) cases.CevapE = testsoru.CevapE;
            if (testsoru.DogruCevap != null) cases.DogruCevap = (int)testsoru.DogruCevap;          
            if (testsoru.TestId != null) cases.TestId = (int)testsoru.TestId;
            if (testsoru.SoruSirasi != null) cases.SoruSirasi = (int)testsoru.SoruSirasi;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, testsoru);
            return Kaydet();
        }

        public bool TestSoruKontrol(int id)
        {
            return _context.TestSorus.Any(x => x.Id == id);
        }

        public ICollection<TestSoru> TestSoruListele()
        {
            return _context.TestSorus.OrderBy(x => x.SoruSirasi).ToList();
        }

        public bool TestSoruSil(TestSoru test)
        {
            _context.TestSorus.Remove(test);
            return Kaydet();
        }
    }
}
