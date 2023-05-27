namespace EgitimPortali.Request.TestSoru
{
    public class TestSoruUpdateRequest
    {
        public int Id { get; set; }
        public int? TestId { get; set; }
        public string? Soru { get; set; }
        public string? CevapA { get; set; }
        public string? CevapB { get; set; }
        public string? CevapC { get; set; }
        public string? CevapD { get; set; }
        public string? CevapE { get; set; }
        public int? DogruCevap { get; set; }
        public int? SoruSirasi { get; set; }

    }
}
