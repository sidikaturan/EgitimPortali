using EgitimPortali.Models;

namespace EgitimPortali.DTO
{
    public class KullaniciRolDto
    {
        public int Id { get; set; }
        public int RolID { get; set; }
        public int KullaniciID { get; set; }
        public Roller Roller { get; set; }
    }
}
