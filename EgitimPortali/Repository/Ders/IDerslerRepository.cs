using EgitimPortali.Models;
using EgitimPortali.Request.Dersler;

namespace EgitimPortali.Repository.Ders
{
    public interface IDerslerRepository
    {
        ICollection<Dersler> DersleriListele();
        Dersler DersGetir(int id);
        string KategoriAdi(int id);
        bool DersKontrol(int id);
        bool DersEkle(Dersler dersler);
        bool DersGuncelle(int Id, DerslerUpdateRequest dersler);
        bool DersSil(Dersler dersler);
        bool Kaydet();
    }
}
