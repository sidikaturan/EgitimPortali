using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.DersIcerikleri;

namespace EgitimPortali.Repository.DersIcerik
{
    public interface IDersIcerikRepository
    {
        ICollection<DersIcerikleri> DersIcerikleriniListele();
        ICollection<DersIcerikleri> DersIcerikleriniListele(int dersiceriklerid);
        ICollection<DersIcerikleri> Son3Ders(int dersiceriklerid);
        DersIcerikleri DersIcerikleriGetir(int id);
        bool DersIcerikleriKontrol(int id);
        bool DersIcerikleriEkle(DersIcerikleri dersIcerikleri);
        bool DersIcerikleriGuncelle(int Id, DersIcerikleriDto dersIcerikleri);
        bool DersIcerikleriSil(DersIcerikleri dersIcerikleri);
        bool Kaydet();
    }
}
