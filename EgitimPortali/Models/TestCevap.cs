using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class TestCevap : BaseModel
    {
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int? TestSoruId { get; set; }
        public TestSoru TestSoru { get; set; }
        public int CevapId { get; set; }
    }
}
