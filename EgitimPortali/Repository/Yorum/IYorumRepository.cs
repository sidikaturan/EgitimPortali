using EgitimPortali.DTO;
using EgitimPortali.Models;

namespace EgitimPortali.Repository.Yorum
{
    public interface IYorumRepository
    {
        ICollection<Yorumlar> YorumlariListele();
        ICollection<Yorumlar> KullaniciyaGoreYorumlariListele();
        ICollection<Yorumlar> IcerikYorumlariniListele(int icerikid);
        Yorumlar YorumGetir(int id);
        bool YorumKontrol(int id);
        bool YorumEkle(Yorumlar yorumlar);
        bool YorumGuncelle(Yorumlar yorumlar);
        bool YorumSil(Yorumlar yorumlar);
        bool Kaydet();
    }
}
