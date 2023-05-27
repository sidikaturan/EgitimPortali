using EgitimPortali.Context;
using EgitimPortali.Models;

namespace EgitimPortali.Repository.Iletisimler
{
    public class IletisimRepository : IIletisimRepository
    {
        private readonly SqlServerDbContext _context;

        public IletisimRepository(SqlServerDbContext context)
        {
            _context = context;
        }
        public bool IletisimEkle(Iletisim iletisim)
        {
            _context.Iletisims.Add(iletisim);
            return Kaydet();
        }

        public Iletisim IletisimGetir(int id)
        {
            return _context.Iletisims.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool IletisimGuncelle(Iletisim iletisim)
        {
            _context.Iletisims.Update(iletisim);
            return Kaydet();
        }

        public bool IletisimKontrol(int id)
        {
            return _context.Iletisims.Any(x => x.Id == id);
        }

        public ICollection<Iletisim> IletisimListele()
        {
            return _context.Iletisims.ToList();
        }

        public bool IletisimSil(Iletisim iletisim)
        {
            _context.Iletisims.Remove(iletisim);
            return Kaydet();
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
