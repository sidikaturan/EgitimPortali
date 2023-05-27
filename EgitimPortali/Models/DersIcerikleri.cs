using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class DersIcerikleri : BaseModel
    {

        public string Name { get; set; }
        public string Icerik { get; set; }
        public string? Resim { get; set; }
        public string? PdfYolu { get; set; }
        public ICollection<Yorumlar> Yorumlar { get; set; }
        public ICollection<DersTakip> DersTakip { get; set; }
        public int KonularID { get; set; }
        public Konular Konular { get; set; }

    }
}
