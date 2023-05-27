using EgitimPortali.Models;

namespace EgitimPortali.DTO
{
    public class SorularDto
    {
        public int Id { get; set; }
        public int DerslerID { get; set; }
        public string Name { get; set; }
        public string Icerik { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dersler Dersler { get; set; }

    }
}
