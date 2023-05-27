using EgitimPortali.Models;

namespace EgitimPortali.DTO
{
    public class YorumlarDto
    {
        public int Id { get; set; }
        public int DersIcerikleriID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DersIcerikleri DersIcerikleri { get; set; }
    }
}
