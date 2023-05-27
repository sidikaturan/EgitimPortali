using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Yorumlar : BaseModel
    {
        public int DersIcerikleriID { get; set; }
        public DersIcerikleri DersIcerikleri { get; set; }
        public string Name { get; set; }
    }
}
