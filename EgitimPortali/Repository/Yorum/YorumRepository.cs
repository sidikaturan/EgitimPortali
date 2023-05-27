using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EgitimPortali.Repository.Yorum
{
    public class YorumRepository : IYorumRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public YorumRepository(SqlServerDbContext context,IMapper mapper, IHttpContextAccessor httpContextAccessor)
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
        public ICollection<Yorumlar> IcerikYorumlariniListele(int icerikid)
        {
            return _context.Yorumlars.Include(x=>x.DersIcerikleri).Where(x => x.DersIcerikleriID == icerikid).ToList();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool YorumEkle(Yorumlar yorumlar)
        {
            _context.Yorumlars.Add(yorumlar);
            return Kaydet();
        }

        public Yorumlar YorumGetir(int id)
        {
            return _context.Yorumlars.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool YorumGuncelle(Yorumlar yorumlar)
        {
            _context.Yorumlars.Update(yorumlar);
            return Kaydet();
        }

        public ICollection<Yorumlar> YorumlariListele()
        {
            return _context.Yorumlars.Include(x=>x.DersIcerikleri).ToList();
        }
      

        public bool YorumKontrol(int id)
        {
            return _context.Yorumlars.Any(x=>x.Id == id);
        }

        public bool YorumSil(Yorumlar yorumlar)
        {
            _context.Yorumlars.Remove(yorumlar);
            return Kaydet();
        }

        public ICollection<Yorumlar> KullaniciyaGoreYorumlariListele()
        {
            return _context.Yorumlars.Include(x => x.DersIcerikleri).Where(x => x.CreatedBy == GetMyName()).ToList();
        }
    }
}