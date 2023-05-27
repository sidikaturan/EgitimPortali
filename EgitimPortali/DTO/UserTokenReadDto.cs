namespace EgitimPortali.DTO
{
    public class UserTokenReadDto
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
