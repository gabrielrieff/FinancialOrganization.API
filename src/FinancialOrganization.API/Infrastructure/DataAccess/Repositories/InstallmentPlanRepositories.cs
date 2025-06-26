using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Infrasctructure.DataAccess;

namespace FinancialOrganization.API.Infrastructure.DataAccess.Repositories;

public class InstallmentPlanRepositories : IInstallmentPlanRepository
{
    private readonly FinancialOrganizationDbContext _dbContext;

    public InstallmentPlanRepositories(FinancialOrganizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Register(InstallmentPlan entity, CancellationToken cancellationToken)
    {
        await _dbContext.InstallmentPlans.AddAsync(entity, cancellationToken);
    }

    public void Update(InstallmentPlan entity, CancellationToken cancellationToken)
    {
        _dbContext.InstallmentPlans.Update(entity);
    }
}
