using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.Hakkimizda;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.Hakkımızda
{
    public class HakkimizdaRepository : IHakkimizdaRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;

        public HakkimizdaRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool HakkimizdaEkle(Hakkimizda hakkimizda)
        {
            _context.Hakkimizdas.Add(hakkimizda);
            return Kaydet();
        }

        public Hakkimizda HakkimizdaGetir(int id)
        {
            return _context.Hakkimizdas.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool HakkimizdaGuncelle(int Id, HakkimizdaUpdateRequest hakkimizda)
        {
            if (hakkimizda == null)
            {
                throw new ArgumentNullException(nameof(hakkimizda));
                return false;
            }

            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
                return false;
            }

            Models.Hakkimizda cases = _context.Hakkimizdas.FirstOrDefault(u => u.Id == Id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(Id));
                return false;
            }

            if (hakkimizda.UstBaslik != null) cases.UstBaslik = hakkimizda.UstBaslik;
            if (hakkimizda.Icerik != null) cases.Icerik = hakkimizda.Icerik;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, hakkimizda);
            return Kaydet();
        }

        public bool HakkimizdaKontrol(int id)
        {
            return _context.Hakkimizdas.Any(x => x.Id == id);
        }

        public ICollection<Hakkimizda> HakkimizdaListele()
        {
            return _context.Hakkimizdas.ToList();
        }

        public bool HakkimizdaSil(Hakkimizda hakkimizda)
        {
            _context.Hakkimizdas.Remove(hakkimizda);
            return Kaydet();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
