using EgitimPortali.Models;
using EgitimPortali.Request.Reklamlar;

namespace EgitimPortali.Repository.Reklam
{
    public interface IReklamRepository
    {
        ICollection<Reklamlar> ReklamlariListele();
        Reklamlar ReklamGetir(int id);
        bool ReklamKontrol(int id);
        bool ReklamEkle(ReklamlarPostRequest reklam);
        bool ReklamGuncelle(int Id,ReklamlarUpdateRequest reklam);
        bool ReklamSil(Reklamlar reklam);
        bool Kaydet();
    }
}
