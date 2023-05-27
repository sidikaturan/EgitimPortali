using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.AnaSayfa;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.Anasayfa
{
    public class AnasayfaRepository : IAnasayfaRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;

        public AnasayfaRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool AnaSayfaEkle(AnaSayfa anaSayfa)
        {
            _context.AnaSayfas.Add(anaSayfa);
            return Kaydet();
        }

        public bool AnaSayfaGuncelle(int id, AnaSayfaUpdateRequest anaSayfa)
        {
            if (anaSayfa == null)
            {
                throw new ArgumentNullException(nameof(anaSayfa));
                return false;
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
                return false;
            }

            Models.AnaSayfa cases = _context.AnaSayfas.FirstOrDefault(u => u.Id == id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(id));
                return false;
            }

            if (anaSayfa.UstBaslik != null) cases.UstBaslik = anaSayfa.UstBaslik;
            if (anaSayfa.Icerik != null) cases.Icerik = anaSayfa.Icerik;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, anaSayfa);
            return Kaydet();
        }

        public bool AnaSayfaKontrol(int id)
        {
            return _context.AnaSayfas.Any(x => x.Id == id);
        }

        public ICollection<AnaSayfa> AnaSayfaListele()
        {
            return _context.AnaSayfas.ToList();
        }

        public bool AnaSayfaSil(AnaSayfa anaSayfa)
        {
            _context.AnaSayfas.Remove(anaSayfa);
            return Kaydet();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public AnaSayfa AnaSayfaGetir(int id)
        {
            return _context.AnaSayfas.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
