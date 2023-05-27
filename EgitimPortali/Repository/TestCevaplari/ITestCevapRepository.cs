using EgitimPortali.DTO;
using EgitimPortali.Models;
using EgitimPortali.Request.TestCevap;

namespace EgitimPortali.Repository.TestCevaplari
{
    public interface ITestCevapRepository
    {
        ICollection<TestCevap> TestCevapListele();
        ICollection<TestCevap> KullaniciTestCevaplariIdListele(int id);
        ICollection<CozulenTestDto> KullaniciTestCevapListele();
        TestCevap TestCevapGetir(int id);
        bool TestCevapKontrol(int id);
        bool TestCevapEkle(TestCevapPostRequest test);
        bool TestCevapSil(TestCevap test);
        bool Kaydet();
    }
}