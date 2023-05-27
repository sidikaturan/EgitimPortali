using EgitimPortali.DTO;

namespace Uygulama.Models
{
    public class SoruDetaylari
    {
        public SorularDto sorular { get; set; }
        public IEnumerable<SoruCevapDto> sorucevap { get; set; }
    }
}
