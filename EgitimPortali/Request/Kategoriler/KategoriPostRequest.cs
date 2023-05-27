using System.ComponentModel.DataAnnotations;

namespace EgitimPortali.Request.Kategoriler
{
    public class KategoriPostRequest
    {
        [Required]
        public String Name { get; set; }
    }
}
