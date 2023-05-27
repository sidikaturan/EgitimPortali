using EgitimPortali.Models;
using EgitimPortali.Request.TestSoru;

namespace EgitimPortali.Repository.TestSorulari
{
    public interface ITestSoruRepository
    {
        ICollection<TestSoru> TestSoruListele();
        ICollection<TestSoru> TesteGoreSoruListele(int id);
        TestSoru TestSoruGetir(int id);
        bool TestSoruKontrol(int id);
        bool TestSoruEkle(TestSoruPostRequest test);
        bool TestSoruGuncelle(int id, TestSoruUpdateRequest test);
        bool TestSoruSil(TestSoru test);
        bool Kaydet();
    }
}
