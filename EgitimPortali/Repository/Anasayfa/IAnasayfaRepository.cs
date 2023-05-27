using EgitimPortali.Models;
using EgitimPortali.Request.AnaSayfa;

namespace EgitimPortali.Repository.Anasayfa
{
    public interface IAnasayfaRepository
    {
        ICollection<AnaSayfa> AnaSayfaListele();
        AnaSayfa AnaSayfaGetir(int id);
        bool AnaSayfaKontrol(int id);
        bool AnaSayfaEkle(AnaSayfa anaSayfa);
        bool AnaSayfaGuncelle(int id, AnaSayfaUpdateRequest anaSayfa);
        bool AnaSayfaSil(AnaSayfa anaSayfa);
        bool Kaydet();
    }
}
