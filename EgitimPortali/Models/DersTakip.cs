using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class DersTakip : BaseModel
    {
        public DersIcerikleri DersIcerikleri { get; set; }
        public int DersIcerikleriId { get; set; }
        public bool Durum { get; set; }
    }
}