namespace EgitimPortali.Request.DersIcerikleri
{
    public class DersIcerikleriPostRequest
    {
        public int KonularID { get; set; }
        public String Name { get; set; }
        public String Icerik { get; set; }
        public String? Resim { get; set; }
        public String? PdfYolu { get; set; }
    }
}
