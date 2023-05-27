using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Sorular : BaseModel
    {
        public int DerslerID { get; set; }
        public Dersler Dersler { get; set; }
        public string Name { get; set; }
        public string Icerik { get; set; }
        public ICollection<SorularinCevaplari> SorularinCevaplari { get; set; }

    }
}
