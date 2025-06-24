using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Security.Tokens;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using FinancialOrganization.API.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly FinancialOrganizationDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(FinancialOrganizationDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        var token = _tokenProvider.TokenOnRequest();

        var tokenHandle = new JwtSecurityTokenHandler();
        var jwtSecuretyToken = tokenHandle.ReadJwtToken(token);

        var userIdendifier = jwtSecuretyToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdendifier == Guid.Parse(userIdendifier));
    }
}
