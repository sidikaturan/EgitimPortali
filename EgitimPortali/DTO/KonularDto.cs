using EgitimPortali.Models;

namespace EgitimPortali.DTO
{
    public class KonularDto
    {
        public int Id { get; set; }
        public int DerslerID { get; set; }
        public string Name { get; set; }
        public string? Resim { get; set; }
        public int KonuSirasi { get; set; }
        public Dersler Dersler { get; set; }
    }
}
