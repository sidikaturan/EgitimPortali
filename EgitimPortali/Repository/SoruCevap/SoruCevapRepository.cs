using EgitimPortali.Context;
using EgitimPortali.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EgitimPortali.Repository.SoruCevap
{
    public class SoruCevapRepository : ISoruCevapRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SoruCevapRepository(SqlServerDbContext context, IHttpContextAccessor httpContextAccessor)
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
        public ICollection<SorularinCevaplari> CevaplariSoralaraGoreGetir(int id)
        {
            return _context.SorularinCevaplaris.Where(x => x.SorularID == id).ToList();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }


        public bool SorularinCevaplariEkle(SorularinCevaplari sorularinCevaplari)
        {
            _context.SorularinCevaplaris.Add(sorularinCevaplari);
            return Kaydet();
        }

        public SorularinCevaplari SorularinCevaplariGetir(int id)
        {
            return _context.SorularinCevaplaris.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool SorularinCevaplariGuncelle(SorularinCevaplari sorularinCevaplari)
        {
            _context.SorularinCevaplaris.Update(sorularinCevaplari);
            return Kaydet();
        }

        public bool SorularinCevaplariKontrol(int id)
        {
            return _context.SorularinCevaplaris.Any(x=>x.Id == id);
        }

        public ICollection<SorularinCevaplari> SorularinCevaplariListele()
        {
            return _context.SorularinCevaplaris.ToList();
        }

        public bool SorularinCevaplariSil(SorularinCevaplari sorularinCevaplari)
        {
            _context.SorularinCevaplaris.Remove(sorularinCevaplari);
            return Kaydet();
        }

        public ICollection<SorularinCevaplari> KullaniciCevaplariSorularaGoreGetir(int id)
        {
            return _context.SorularinCevaplaris.Where(x => x.SorularID == id).ToList();
        }

        public bool SoruCevapGuncelle(int id)
        {
            var deger = _context.SorularinCevaplaris.Where(x => x.Id == id).FirstOrDefault();
            if (deger.Dogruluk == true)
                deger.Dogruluk = false;
            else
                deger.Dogruluk = true;
            return Kaydet();
        }
    }
}
