using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class TestSoru : BaseModel
    {
        public int TestId { get; set; }
        public Test Test { get; set; }
        public string Soru { get; set; }
        public string? SoruGorsel { get; set; }
        public string CevapA { get; set; }
        public string? GorselA { get; set; }
        public string CevapB { get; set; }
        public string? GorselB { get; set; }
        public string CevapC { get; set; }
        public string? GorselC { get; set; }
        public string CevapD { get; set; }
        public string? GorselD { get; set; }
        public string CevapE { get; set; }
        public string? GorselE { get; set; }
        public int DogruCevap { get; set; }
        public int SoruSirasi { get; set; }
        public ICollection<TestCevap> TestinCevaplari { get; set; }
    }
}
