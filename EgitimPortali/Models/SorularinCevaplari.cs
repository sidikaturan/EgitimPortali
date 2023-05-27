using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class SorularinCevaplari : BaseModel
    {
        public int SorularID { get; set; }
        public Sorular Sorular { get; set; }
        public string Icerik { get; set; }
        public bool Dogruluk { get; set; }
    }
}
