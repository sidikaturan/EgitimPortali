using EgitimPortali.Models;
using EgitimPortali.Request.Test;

namespace EgitimPortali.Repository.Testler
{
    public interface ITestRepository
    {
        ICollection<Test> TestListele();
        ICollection<Test> KonuyaGoreTestListele(int id);
        Test TestGetir(int id);
        bool TestKontrol(int id);
        bool KullaniciCozumKontrol(int id);
        bool TestEkle(TestPostRequest test);
        bool TestGuncelle(int id, TestUpdateRequest test);
        bool TestSil(Test test);
        bool Kaydet();
    }
}
