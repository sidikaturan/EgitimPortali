namespace EgitimPortali.Request.TestSoru
{
    public class TestSoruPostRequest
    {
        public int TestId { get; set; }
        public string Soru { get; set; }
        public string CevapA { get; set; }
        public string CevapB { get; set; }
        public string CevapC { get; set; }
        public string CevapD { get; set; }
        public string CevapE { get; set; }
        public string? SoruGorsel { get; set; }
        public string? GorselA { get; set; }
        public string? GorselB { get; set; }
        public string? GorselC { get; set; }
        public string? GorselD { get; set; }
        public string? GorselE { get; set; }
        public int DogruCevap { get; set; }
    }
}
