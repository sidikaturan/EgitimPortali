using EgitimPortali.Models.Base;

namespace EgitimPortali.Models
{
    public class Dersler : BaseModel
    {
   
        public string Name { get; set; }
        public int KategorilerID { get; set; }
        public Kategoriler Kategoriler { get; set; }  
        public ICollection<Konular> Konulars { get; set; }
        public ICollection<Sorular> Sorulars { get; set; }
    }
}
