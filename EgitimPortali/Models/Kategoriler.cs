using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Kategoriler : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Dersler> Derslers { get; set; }
    }
}
