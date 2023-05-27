using EgitimPortali.Models;

namespace EgitimPortali.Repository.Soru
{
    public interface ISoruRepository
    {
        ICollection<Sorular> SorulariListele();
        ICollection<Sorular> kullaniciyaGoreSorulariListele();
        ICollection<Sorular> DerslereGoreSoruListeleme(int dersid);
        Sorular SoruGetir(int id);
        Sorular KullaniciSoruGetir(int id);
        bool SoruKontrol(int id);
        bool SoruEkle(Sorular sorular);
        bool SoruGuncelle(Sorular sorular);
        bool SoruSil(Sorular sorular);
        bool Kaydet();
    }
}
