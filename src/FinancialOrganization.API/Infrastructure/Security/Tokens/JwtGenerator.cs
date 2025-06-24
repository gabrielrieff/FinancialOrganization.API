using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace FinancialOrganization.API.Infrastructure.Security.Tokens;

internal class JwtGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signingKey;

    public JwtGenerator(uint expirationTimeMinutes, string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.UserIdendifier.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandle = new JwtSecurityTokenHandler();
        var securetyToken = tokenHandle.CreateToken(tokenDescriptor);

        return tokenHandle.WriteToken(securetyToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(key);
    }
}
