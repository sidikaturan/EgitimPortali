namespace EgitimPortali.DTO
{
    public class DersIcerikleriDto
    {
        public int Id { get; set; }
        public int KonularID { get; set; }
        public string Name { get; set; }
        public string Icerik { get; set; }
        public string Resim { get; set; }
        public string? PdfYolu { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
