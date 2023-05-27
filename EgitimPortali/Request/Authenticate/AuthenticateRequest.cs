using System.ComponentModel.DataAnnotations;

namespace EgitimPortali.Request.Authenticate
{
    public class AuthenticateRequest
    {
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
}
