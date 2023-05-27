using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.Kategoriler;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.Kategori
{
    public class KategorilerRepository : IKategorilerRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;

        public KategorilerRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool KategoriEkle(Kategoriler category)
        {
            _context.Kategorilers.Add(category);
            return Kaydet();
        }

        public Kategoriler KategoriGetir(int id)
        {
            return _context.Kategorilers.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool KategoriGuncelle(int Id,KategoriUpdateRequest category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
                return false;
            }

            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
                return false;
            }

            Models.Kategoriler cases = _context.Kategorilers.FirstOrDefault(u => u.Id == Id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(Id));
                return false;
            }

            if (category.Name != null) cases.Name = category.Name;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, category);
            return Kaydet();
        }

        public bool KategoriKontrol(int id)
        {
            return _context.Kategorilers.Any(x => x.Id == id);
        }

        public ICollection<Kategoriler> KategorileriListele()
        {
            return _context.Kategorilers.ToList();
        }

        public bool KategoriSil(Kategoriler category)
        {
            _context.Kategorilers.Remove(category);
            return Kaydet();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}