using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.Repositories.Users;

public interface IUserRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<User?> GetByEmail(string email);
    public Task Register(User entity, CancellationToken cancellationToken);
    public void Update(User entity, CancellationToken cancellationToken);
    public Task<User?> GetById(Guid id, CancellationToken cancellationToken);
    public void Delete(User entity, CancellationToken cancellationToken);
}
