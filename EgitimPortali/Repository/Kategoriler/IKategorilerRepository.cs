using EgitimPortali.Models;
using EgitimPortali.Request.Kategoriler;

namespace EgitimPortali.Repository.Kategori
{
    public interface IKategorilerRepository
    {
        ICollection<Kategoriler> KategorileriListele();
        Kategoriler KategoriGetir(int id);
        bool KategoriKontrol(int id);
        bool KategoriEkle(Kategoriler category);
        bool KategoriGuncelle(int Id,KategoriUpdateRequest category);
        bool KategoriSil(Kategoriler category);
        bool Kaydet();
    }
}
