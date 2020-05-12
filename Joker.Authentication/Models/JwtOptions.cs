namespace Joker.Authentication.Models
{
    public class JwtOptions
    {
        public string AuthenticationScheme { get; set; }
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateAudience { get; set; }
        public string ValidAudience { get; set; }
    }
}