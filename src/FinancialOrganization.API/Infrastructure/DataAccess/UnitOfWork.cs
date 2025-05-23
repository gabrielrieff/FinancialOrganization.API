using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Infrasctructure.DataAccess;

namespace FinancialOrganization.API.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly FinancialOrganizationDbContext _dbContext;

    public UnitOfWork(FinancialOrganizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync();
    }
}
