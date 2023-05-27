using EgitimPortali.DTO;

namespace Uygulama.Models
{
    public class OgrenciDersTakip
    {
        public IEnumerable<KonularDto> Konular { get; set; }
        public IEnumerable<DersIcerikleriDto> DersIcerikleri { get; set; }
        public IEnumerable<DersTakipDto> DersTakip { get; set; }

    }
}
