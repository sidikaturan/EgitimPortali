using EgitimPortali.Models;
using EgitimPortali.Request.Konular;

namespace EgitimPortali.Repository.Konu
{
    public interface IKonularRepository
    {
        ICollection<Konular> KonulariListele();
        ICollection<Konular> KonulariListele(int dersid);
        Konular KonuGetir(int id);
        string KonuName(int id);
        bool KonuKontrol(int id);
        bool KonuEkle(Konular konular);
        bool KonuGuncelle(int Id,KonularUpdateRequest konular);
        bool KonuSil(Konular konular);
        bool Kaydet();
    }
}
