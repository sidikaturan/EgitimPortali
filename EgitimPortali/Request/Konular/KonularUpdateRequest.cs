namespace EgitimPortali.Request.Konular
{
    public class KonularUpdateRequest
    {
        public int Id { get; set; }
        public int DerslerID { get; set; }
        public string Name { get; set; }
        public string? Resim { get; set; }
        public int? KonuSirasi { get; set; }

    }
}
