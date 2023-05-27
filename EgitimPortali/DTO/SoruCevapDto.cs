using EgitimPortali.Models;

namespace EgitimPortali.DTO
{
    public class SoruCevapDto
    {
        public int Id { get; set; }
        public int SorularID { get; set; }
        public string Icerik { get; set; }
        public bool Dogruluk { get; set; }
        public DateTime CreatedAt { get; set; }
        public DersIcerikleri DersIcerikleri { get; set; }
    }
}
