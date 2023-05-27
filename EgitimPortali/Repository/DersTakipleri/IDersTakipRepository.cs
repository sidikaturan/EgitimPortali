using EgitimPortali.Models;
using EgitimPortali.Request.DersTakipleri;
using EgitimPortali.Request.Hakkimizda;

namespace EgitimPortali.Repository.DersTakipleri
{
    public interface IDersTakipRepository
    {
        ICollection<DersTakip> DersTakipListele();
        ICollection<DersTakip> KullaniciyaGoreDersTakipListele();
        DersTakip DersTakipGetir(int id);
        bool DersTakipKontrol(int id);
        bool KullaniciDersTakipKontrol(int id);
        bool DersTakipEkle(int id);
        bool DersTakipGuncelle(int id);
        bool DersTakipSil(DersTakip derstakip);
        bool Kaydet();
    }
}
