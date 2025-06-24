namespace FinancialOrganization.API.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}
