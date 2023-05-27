using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.KullanicilarinRolleri;

namespace EgitimPortali.Repository.KullaniciRol
{
    public interface IKullaniciRolRepository
    {
        ICollection<KullanicilarinRolleri> KullanicilarinRolleriListele();
        ICollection<KullaniciRolDto> KullanicininRolleri(int id);
        public IEnumerable<string> GetRoleByUserId(int userId);
        KullanicilarinRolleri KullanicilarinRolleriGetir(int id);
        bool YeniRolEkle(int id);
        bool KullanicilarinRolKontrol(KullanicilarinRolleriPostRequest p);
        bool KullanicilarinRolEkle(KullanicilarinRolleri kullanicilarinRolleri);
        bool KullanicilarinRolGuncelle(int Id,KullanicilarinRolleriUpdateRequest kullanicilarinRolleri);
        bool KullanicilarinRolSil(KullanicilarinRolleri p);
        bool Kaydet();
    }
}
