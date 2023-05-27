using AutoMapper;
using EgitimPortali.Context;
using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Authenticate;
using EgitimPortali.Request.Kullanicilar;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace EgitimPortali.Repository.Kullanici
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KullaniciRepository(SqlServerDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool Kaydet()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool KullaniciEkle(Kullanicilar kullanicilar)
        {
            _context.Kullanicilars.Add(kullanicilar);
            return Kaydet();
        }

        public KullaniciReadDto KullaniciGetir(int id)
        {
            return _mapper.Map<KullaniciReadDto>(_context.Kullanicilars.Where(x => x.Id == id).FirstOrDefault());
        }

        public bool KullaniciGuncelle(int Id,KullanicilarUpdateRequest kullanicilar)
        {
            if (kullanicilar == null)
            {
                throw new ArgumentNullException(nameof(kullanicilar));
                return false;
            }

            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
                return false;
            }

            Models.Kullanicilar cases = _context.Kullanicilars.FirstOrDefault(u => u.Id == Id);

            if (cases == null)
            {
                throw new KeyNotFoundException(nameof(Id));
                return false;
            }

            if (kullanicilar.Ad != null) cases.Ad = kullanicilar.Ad;
            if (kullanicilar.Soyad != null) cases.Soyad = kullanicilar.Soyad;
            if (kullanicilar.Mail != null) cases.Mail = kullanicilar.Mail;
            _context.Entry(cases).State = EntityState.Modified;
            _mapper.Map(cases, kullanicilar);
            return Kaydet();
        }

        public bool KullaniciKontrol(int id)
        {
            return _context.Kullanicilars.Any(x=>x.Id == id);
        }

        public ICollection<Kullanicilar> KullanicilariListele()
        {
            return _context.Kullanicilars.ToList();
        }

        public bool KullaniciSil(int id)
        {
            Models.Kullanicilar kullanici = _context.Kullanicilars.FirstOrDefault(u => u.Id == id);
            _context.Kullanicilars.Remove(kullanici);
            return Kaydet();
        }

        public int Login(AuthenticateRequest user)
        {
            var users = _context.Kullanicilars.SingleOrDefault(x => x.Mail == user.Mail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(user.Sifre, users.Sifre))
                return 0;

            return users.Id;
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

        public KullaniciReadDto Register(KullanicilarPostRequest userPostRequest)
        {
            if (userPostRequest == null)
            {
                throw new ArgumentNullException(nameof(userPostRequest));
            }
            userPostRequest.Sifre = BCrypt.Net.BCrypt.HashPassword(userPostRequest.Sifre);
            Models.Kullanicilar user = _mapper.Map<Models.Kullanicilar>(userPostRequest);
            _context.Kullanicilars.Add(user);
            Kaydet();
            return _mapper.Map<KullaniciReadDto>(user);

        }

        public UserTokenReadDto GetByIdRefreshId(int Id)
        {
            return _mapper.Map<UserTokenReadDto>(_context.Kullanicilars.FirstOrDefault(u => u.Id == Id));
        }

        public bool TokenChange(RefreshToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
                return false;
            }

            if (token.Id == null)
            {
                throw new ArgumentNullException(nameof(token.Id));
                return false;
            }

            Models.Kullanicilar user = _context.Kullanicilars.FirstOrDefault(u => u.Id == token.Id);

            if (user == null)
            {
                throw new KeyNotFoundException(nameof(token.Id));
                return false;
            }

            if (token.Token != null) user.RefreshToken = token.Token;
            if (token.Created != null) user.TokenCreated = token.Created;
            if (token.Expires != null) user.TokenExpires = token.Expires;
            _context.Entry(user).State = EntityState.Modified;
            Kaydet();
            _mapper.Map(user, token);
            return true;
        }
      
        public KullaniciReadDto OturumGetir()
        {
            return _mapper.Map<KullaniciReadDto>(_context.Kullanicilars.Where(x => x.Id == GetMyName()).FirstOrDefault());
        }
    }
}
