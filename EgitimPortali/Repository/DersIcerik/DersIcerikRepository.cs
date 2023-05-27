using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.DersIcerikleri;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.DersIcerik
{
    public class DersIcerikRepository : IDersIcerikRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;

        public DersIcerikRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool DersIcerikleriEkle(DersIcerikleri dersIcerikleri)
        {
            _context.DersIcerikleris.Add(dersIcerikleri);
            return Kaydet();
        }

        public DersIcerikleri DersIcerikleriGetir(int id)
        {
            return _context.DersIcerikleris.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool DersIcerikleriGuncelle(int Id, DersIcerikleriDto dersIcerikleri)
        {
            if (dersIcerikleri == null)
            {
                throw new ArgumentNullException(nameof(dersIcerikleri));
                return false;
            }

            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
                return false;
            }

            Models.DersIcerikleri cases = _context.DersIcerikleris.FirstOrDefault(u => u.Id == Id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(Id));
                return false;
            }

            if (dersIcerikleri.KonularID != null) cases.KonularID = (int)dersIcerikleri.KonularID;
            if (dersIcerikleri.Name != null) cases.Name = dersIcerikleri.Name;
            if (dersIcerikleri.Resim != null) cases.Resim = dersIcerikleri.Resim;
            if (dersIcerikleri.PdfYolu != null) cases.PdfYolu = dersIcerikleri.PdfYolu;
            if (dersIcerikleri.Icerik != null) cases.Icerik = dersIcerikleri.Icerik;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, dersIcerikleri);
            return Kaydet();
        }

        public bool DersIcerikleriKontrol(int id)
        {
            return _context.DersIcerikleris.Any(x=>x.Id == id);
        }

        public ICollection<DersIcerikleri> DersIcerikleriniListele()
        {
            return _context.DersIcerikleris.ToList(); 
        }

        public ICollection<DersIcerikleri> DersIcerikleriniListele(int dersiceriklerid)
        {
            return _context.DersIcerikleris.Where(x => x.KonularID == dersiceriklerid).ToList();
        }

        public bool DersIcerikleriSil(DersIcerikleri dersIcerikleri)
        {
            _context.DersIcerikleris.Remove(dersIcerikleri);
            return Kaydet();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public ICollection<DersIcerikleri> Son3Ders(int dersiceriklerid)
        {
            return _context.DersIcerikleris.Where(x => x.KonularID == dersiceriklerid).Take(3).ToList();
        }
    }
}
