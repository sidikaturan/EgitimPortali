using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.Dersler;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.Ders
{
    public class DerslerRepository : IDerslerRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;

        public DerslerRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool DersEkle(Dersler dersler)
        {
            _context.Derslers.Add(dersler);
            return Kaydet();
        }

        public Dersler DersGetir(int id)
        {
            return _context.Derslers.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool DersGuncelle(int Id, DerslerUpdateRequest dersler)
        {
            if (dersler == null)
            {
                throw new ArgumentNullException(nameof(dersler));
                return false;
            }

            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
                return false;
            }

            Models.Dersler cases = _context.Derslers.FirstOrDefault(u => u.Id == Id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(Id));
                return false;
            }

            if (dersler.Name != null) cases.Name = dersler.Name;
            if (dersler.KategorilerID != null) cases.KategorilerID = (int)dersler.KategorilerID;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, dersler);
            return Kaydet();
        }

        public bool DersKontrol(int id)
        {
            return _context.Derslers.Any(x => x.Id == id);
        }

        public ICollection<Dersler> DersleriListele()
        {
            return _context.Derslers.ToList();
        }

        public bool DersSil(Dersler dersler)
        {
            _context.Derslers.Remove(dersler);
            return Kaydet();
        }

        public string KategoriAdi(int id)
        {
            var deger = _context.Derslers.Where(x => x.Id == id).FirstOrDefault();
            var deger2 = _context.Kategorilers.Where(x => x.Id == deger.KategorilerID).FirstOrDefault();
            return deger2.Name;
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
