using EgitimPortali.Models;
using EgitimPortali.Request.Konular;

namespace EgitimPortali.Repository.SoruCevap
{
    public interface ISoruCevapRepository
    {
        ICollection<SorularinCevaplari> SorularinCevaplariListele();
        ICollection<SorularinCevaplari> CevaplariSoralaraGoreGetir(int id);
        ICollection<SorularinCevaplari> KullaniciCevaplariSorularaGoreGetir(int id);
        SorularinCevaplari SorularinCevaplariGetir(int id);
        bool SoruCevapGuncelle(int id);

        bool SorularinCevaplariKontrol(int id);
        bool SorularinCevaplariEkle(SorularinCevaplari sorularinCevaplari);
        bool SorularinCevaplariGuncelle(SorularinCevaplari sorularinCevaplari);
        bool SorularinCevaplariSil(SorularinCevaplari sorularinCevaplari);
        bool Kaydet();
    }
}
