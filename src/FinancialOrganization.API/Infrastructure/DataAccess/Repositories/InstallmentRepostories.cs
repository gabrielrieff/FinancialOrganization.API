using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;
using FinancialOrganization.API.Infrasctructure.DataAccess;

namespace FinancialOrganization.API.Infrastructure.DataAccess.Repositories;

public class InstallmentRepostories : IInstallmentRepository
{
    private readonly FinancialOrganizationDbContext _dbContext;

    public InstallmentRepostories(FinancialOrganizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Delete(Installment entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Installment?> GetById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Register(List<Installment> entities, CancellationToken cancellationToken)
    {
        await _dbContext.Installments.AddRangeAsync(entities);
    }

    public Task<SearchOutput<Installment>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Update(List<Installment> entities, CancellationToken cancellationToken)
    {
        _dbContext.Installments.UpdateRange(entities);
    }
}
