using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Kullanicilar : BaseModel
    {
        public string Mail { get; set; }
        public string Sifre { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public ICollection<KullanicilarinRolleri> KullanicilarinRolleris { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
