using EgitimPortali.DTO;

namespace Uygulama.Models
{
    public class OgrenciCevap
    {
        public IEnumerable<TestSoruDto> Test { get; set; }
        public IEnumerable<TestCevapDto> TestCevap { get; set; }
    }
}
