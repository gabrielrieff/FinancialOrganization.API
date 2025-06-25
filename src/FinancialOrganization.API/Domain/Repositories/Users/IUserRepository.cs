using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.Repositories.Users;

public interface IUserRepository : IGenegicRepository<User>
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<User?> GetByEmail(string email);
}
