using EgitimPortali.DTO;

namespace Uygulama.Models
{
    public class KonuIcerikleris
    {
        public IEnumerable<DersIcerikleriDto> dersIcerikleriDtos  { get; set; }
        public IEnumerable<TestDto> Tests { get; set; }
    }
}
