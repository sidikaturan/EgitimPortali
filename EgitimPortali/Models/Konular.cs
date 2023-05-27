using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Konular : BaseModel
    {
        public int DerslerID { get; set; }
        public Dersler Dersler { get; set; }
        public string Name { get; set; }
        public string? Resim { get; set; }
        public int KonuSirasi { get; set; }
        public ICollection<DersIcerikleri> DersIcerikleris { get; set; }

    }
}
