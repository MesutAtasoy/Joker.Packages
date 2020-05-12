using System.Collections.Generic;
using Joker.Authentication.Models;

namespace Joker.Authentication.Handlers
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, List<string> roles = null, IDictionary<string, string> claims = null);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}