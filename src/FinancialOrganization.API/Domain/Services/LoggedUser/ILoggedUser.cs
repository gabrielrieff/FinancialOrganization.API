using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> Get();
}
