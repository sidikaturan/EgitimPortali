namespace EgitimPortali.Request.Kullanicilar
{
    public class KullanicilarUpdateRequest
    {
        public int Id { get; set; }
        public String? Mail { get; set; }
        public String? Ad { get; set; }
        public String? Soyad { get; set; }
    }
}
