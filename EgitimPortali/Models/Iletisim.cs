using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Iletisim : BaseModel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Mail { get; set; }
        public string Mesaj { get; set; }
        public string Konu { get; set; }
    }
}