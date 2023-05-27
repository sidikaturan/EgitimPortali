using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.KullanicilarinRolleri;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.KullaniciRol
{
    public class KullaniciRolRepository : IKullaniciRolRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;

        public KullaniciRolRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<string> GetRoleByUserId(int userId)
        {
            var userRole = _context.KullanicilarinRolleris.Include(x => x.Roller).Where(x => x.KullaniciID == userId).Select(x => x.Roller.Name).ToList();
            return userRole;
        }

        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool KullanicilarinRolEkle(KullanicilarinRolleri kullanicilarinRolleri)
        {
            _context.KullanicilarinRolleris.Add(kullanicilarinRolleri);
            return Kaydet();
        }
        

        public bool KullanicilarinRolGuncelle(int Id, KullanicilarinRolleriUpdateRequest kullanicilarinRolleri)
        {
            if (kullanicilarinRolleri == null)
            {
                throw new ArgumentNullException(nameof(kullanicilarinRolleri));
                return false;
            }

            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
                return false;
            }

            Models.KullanicilarinRolleri cases = _context.KullanicilarinRolleris.FirstOrDefault(u => u.Id == Id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(Id));
                return false;
            }

            if (kullanicilarinRolleri.KullaniciID != null) cases.KullaniciID = (int)kullanicilarinRolleri.KullaniciID;
            if (kullanicilarinRolleri.RolID != null) cases.RolID = (int)kullanicilarinRolleri.RolID;
         
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, kullanicilarinRolleri);
            return Kaydet();
        }

        public bool KullanicilarinRolKontrol(KullanicilarinRolleriPostRequest p)
        {
            var deger = _context.KullanicilarinRolleris.Where(x => x.KullaniciID == p.KullaniciID && x.RolID == p.RolID).FirstOrDefault();
            if (deger != null)
                return false;
            else
                return true;
        }
        
        public KullanicilarinRolleri KullanicilarinRolleriGetir(int id)
        {
            return _context.KullanicilarinRolleris.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<KullanicilarinRolleri> KullanicilarinRolleriListele()
        {
            return _context.KullanicilarinRolleris.ToList();
        }

        public bool KullanicilarinRolSil(KullanicilarinRolleri p)
        {
            _context.KullanicilarinRolleris.Remove(p);
            return Kaydet();
        }

        public ICollection<KullaniciRolDto> KullanicininRolleri(int id)
        {
            return _mapper.Map<List<KullaniciRolDto>>(_context.KullanicilarinRolleris.Include(x=>x.Roller).Where(x => x.KullaniciID == id).ToList());
        }

        public bool YeniRolEkle(int id)
        {
            KullanicilarinRolleriPostRequest p = new KullanicilarinRolleriPostRequest();
            p.KullaniciID = id;
            p.RolID = 1;
            var deger = _mapper.Map<KullanicilarinRolleri>(p);
            _context.KullanicilarinRolleris.Add(deger);
            return Kaydet();
        }
    }
}
