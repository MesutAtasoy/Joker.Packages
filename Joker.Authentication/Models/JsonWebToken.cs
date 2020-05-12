using System.Collections.Generic;

namespace Joker.Authentication.Models
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expires { get; set; }
        public string Id { get; set; }
        public List<string> Role { get; set; }
        public IDictionary<string, string> Claims { get; set; }
    }
}