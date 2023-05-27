using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Test : BaseModel
    {
        public int KonularID { get; set; }
        public Konular Konular { get; set; }
        public string Name { get; set; }
        public ICollection<TestSoru> TestSorulari { get; set; }
        public ICollection<TestCevap> TestinCevaplari { get; set; }
    }
}