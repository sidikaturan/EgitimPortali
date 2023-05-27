using EgitimPortali.Context;
using EgitimPortali.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EgitimPortali.Repository.Soru
{
    public class SoruRepository : ISoruRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SoruRepository(SqlServerDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
        public ICollection<Sorular> DerslereGoreSoruListeleme(int dersid)
        {
            return _context.Sorulars.Where(x => x.DerslerID == dersid).ToList();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public ICollection<Sorular> kullaniciyaGoreSorulariListele()
        {
            return _context.Sorulars.Include(x => x.Dersler).Where(x => x.CreatedBy == GetMyName()).ToList();
        }

        public bool SoruEkle(Sorular sorular)
        {
            _context.Sorulars.Add(sorular);
            return Kaydet();
        }

        public Sorular SoruGetir(int id)
        {
            return _context.Sorulars.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool SoruGuncelle(Sorular sorular)
        {
            _context.Sorulars.Update(sorular);
            return Kaydet();
        }

        public bool SoruKontrol(int id)
        {
            return _context.Sorulars.Any(x => x.Id == id);
        }

        public ICollection<Sorular> SorulariListele()
        {
            return _context.Sorulars.Include(x=>x.Dersler).ToList();
        }

        public bool SoruSil(Sorular sorular)
        {
            _context.Sorulars.Remove(sorular);
            return Kaydet();
        }

        public Sorular KullaniciSoruGetir(int id)
        {
            return _context.Sorulars.Where(x => x.Id == id && x.CreatedBy == GetMyName()).FirstOrDefault();
        }
    }
}
