using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Roller : BaseModel
    {
        public string Name { get; set; }
        public ICollection<KullanicilarinRolleri> KullanicilarinRolleris { get; set; }

    }
}
