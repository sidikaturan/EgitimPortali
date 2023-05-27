using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.Authenticate;
using EgitimPortali.Request.Kullanicilar;

namespace EgitimPortali.Repository.Kullanici
{
    public interface IKullaniciRepository
    {
        ICollection<Kullanicilar> KullanicilariListele();
        KullaniciReadDto KullaniciGetir(int id);
        KullaniciReadDto OturumGetir();

        bool KullaniciKontrol(int id);
        bool KullaniciEkle(Kullanicilar kullanicilar);
        bool KullaniciGuncelle(int Id,KullanicilarUpdateRequest kullanicilar);
        bool KullaniciSil(int id);
        bool Kaydet();
        public int Login(AuthenticateRequest user);
        public int GetMyName();
        KullaniciReadDto Register(KullanicilarPostRequest userPostRequest);
        public UserTokenReadDto GetByIdRefreshId(int Id);
        public bool TokenChange(RefreshToken token);


    }
}
