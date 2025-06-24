using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories.Users;
using FinancialOrganization.API.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Infrastructure.DataAccess.Repositories;

public class UserRepositories : IUserRepository
{
    private readonly FinancialOrganizationDbContext _dbContext;

    public UserRepositories(FinancialOrganizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Delete(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public Task<User?> GetById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Register(User entity, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(entity, cancellationToken);
    }

    public void Update(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
