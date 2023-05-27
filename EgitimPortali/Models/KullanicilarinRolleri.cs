using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class KullanicilarinRolleri : BaseModel
    {
        public int RolID { get; set; }
        public Roller Roller { get; set; }
        public int KullaniciID { get; set; }
        public Kullanicilar Kullanicilar { get; set; }

    }
}
