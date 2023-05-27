using EgitimPortali.Models;

namespace EgitimPortali.Repository.Iletisimler
{
    public interface IIletisimRepository
    {
        ICollection<Iletisim> IletisimListele();
        Iletisim IletisimGetir(int id);
        bool IletisimKontrol(int id);
        bool IletisimEkle(Iletisim iletisim);
        bool IletisimGuncelle(Iletisim iletisim);
        bool IletisimSil(Iletisim iletisim);
        bool Kaydet();
    }
}
