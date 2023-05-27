using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.Reklamlar;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.Reklam
{
    public class ReklamRepository : IReklamRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;

        public ReklamRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool ReklamEkle(ReklamlarPostRequest reklam)
        {
            if (reklam.Gorsel != null)
            {
                Models.Reklamlar media = _mapper.Map<Models.Reklamlar>(reklam);
                _context.Reklamlars.Add(media);
                return Kaydet();
            }
            else
                return Kaydet();
        }

        public Reklamlar ReklamGetir(int id)
        {
            return _context.Reklamlars.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool ReklamGuncelle(int Id, ReklamlarUpdateRequest reklam)
        {
            if (reklam == null)
            {
                throw new ArgumentNullException(nameof(reklam));
                return false;
            }

            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
                return false;
            }

            Models.Reklamlar cases = _context.Reklamlars.FirstOrDefault(u => u.Id == Id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(Id));
                return false;
            }

            if (reklam.Icerik != null) cases.Icerik = reklam.Icerik;
            if (reklam.UstBaslik != null) cases.UstBaslik = reklam.UstBaslik;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, reklam);
            return Kaydet();
        }

        public bool ReklamKontrol(int id)
        {
            return _context.Reklamlars.Any(x => x.Id == id);
        }

        public ICollection<Reklamlar> ReklamlariListele()
        {
            return _context.Reklamlars.ToList();
        }

        public bool ReklamSil(Reklamlar reklam)
        {
            _context.Reklamlars.Remove(reklam);
            return Kaydet();
        }
    }
}
