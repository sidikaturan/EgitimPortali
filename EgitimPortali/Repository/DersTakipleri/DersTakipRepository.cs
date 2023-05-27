using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.DersTakipleri;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EgitimPortali.Repository.DersTakipleri
{
    public class DersTakipRepository : IDersTakipRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DersTakipRepository(SqlServerDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
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
        public bool DersTakipEkle(int id)
        {
            DersTakip cs = new DersTakip();
            cs.DersIcerikleriId = id;
            cs.Durum = true;
            _context.DersTakips.Add(cs);
            return Kaydet();
        }

        public DersTakip DersTakipGetir(int id)
        {
            return _context.DersTakips.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool DersTakipGuncelle(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
                return false;
            }


            Models.DersTakip cases = _context.DersTakips.FirstOrDefault(u => u.Id == id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(id));
                return false;
            }

            if (id != null && cases.CreatedBy == GetMyName())
            {
                if (cases.Durum == true)
                    cases.Durum = false;
                else
                    cases.Durum = true;
            }
            _context.Entry(cases).State = EntityState.Modified;
            return Kaydet();
        }

        public bool DersTakipKontrol(int id)
        {
            return _context.DersTakips.Any(x => x.Id == id);
        }

        public ICollection<DersTakip> DersTakipListele()
        {
            return _context.DersTakips.ToList();
        }

        public bool DersTakipSil(DersTakip derstakip)
        {
            _context.DersTakips.Remove(derstakip);
            return Kaydet();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public ICollection<DersTakip> KullaniciyaGoreDersTakipListele()
        {
            return _context.DersTakips.Where(x => x.CreatedBy == GetMyName()).ToList();
        }

        public bool KullaniciDersTakipKontrol(int id)
        {
            var deger = _context.DersTakips.Where(x => x.CreatedBy == GetMyName() && x.DersIcerikleriId == id).FirstOrDefault();
            if (deger != null)
            {
                if (deger.Durum == true)
                    deger.Durum = false;
                else
                    deger.Durum = true;
                Kaydet();
                return false;
            }
            else
                return true;
        }
    }
}