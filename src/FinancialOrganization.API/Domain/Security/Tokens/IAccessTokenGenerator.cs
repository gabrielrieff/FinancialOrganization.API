using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}
