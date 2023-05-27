using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.Models;
using EgitimPortali.Request.Roller;
using Microsoft.EntityFrameworkCore;

namespace EgitimPortali.Repository.Rol
{
    public class RolRepository : IRolRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;

        public RolRepository(SqlServerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool RolEkle(Roller roller)
        {
            _context.Rollers.Add(roller);
            return Kaydet();
        }

        public Roller RolGetir(int id)
        {
            return _context.Rollers.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool RolGuncelle(int Id,RollerUpdateRequest roller)
        {
            if (roller == null)
            {
                throw new ArgumentNullException(nameof(roller));
                return false;
            }

            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
                return false;
            }

            Models.Roller cases = _context.Rollers.FirstOrDefault(u => u.Id == Id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(Id));
                return false;
            }

            if (roller.Name != null) cases.Name = roller.Name;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, roller);
            return Kaydet();
        }

        public bool RolKontrol(int id)
        {
            return _context.Rollers.Any(x=>x.Id == id);
        }

        public ICollection<Roller> RolleriListele()
        {
            return _context.Rollers.ToList();
        }

        public bool RolSil(Roller roller)
        {
            _context.Rollers.Remove(roller);
            return Kaydet();
        }
    }
}
