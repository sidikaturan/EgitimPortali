using EgitimPortali.Models;
using EgitimPortali.Request.Roller;

namespace EgitimPortali.Repository.Rol
{
    public interface IRolRepository
    {
        ICollection<Roller> RolleriListele();
        Roller RolGetir(int id);
        bool RolKontrol(int id);
        bool RolEkle(Roller roller);
        bool RolGuncelle(int Id,RollerUpdateRequest roller);
        bool RolSil(Roller roller);
        bool Kaydet();
    }
}
