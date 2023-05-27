using EgitimPortali.Models;
using EgitimPortali.Request.Hakkimizda;

namespace EgitimPortali.Repository.Hakkımızda
{
    public interface IHakkimizdaRepository
    {
        ICollection<Hakkimizda> HakkimizdaListele();
        Hakkimizda HakkimizdaGetir(int id);
        bool HakkimizdaKontrol(int id);
        bool HakkimizdaEkle(Hakkimizda hakkimizda);
        bool HakkimizdaGuncelle(int Id,HakkimizdaUpdateRequest hakkimizda);
        bool HakkimizdaSil(Hakkimizda hakkimizda);
        bool Kaydet();
    }
}
