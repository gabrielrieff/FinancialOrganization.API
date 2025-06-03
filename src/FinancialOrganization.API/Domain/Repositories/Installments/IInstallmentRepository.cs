using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Domain.Repositories.Installments;

public interface IInstallmentRepository : ISearchableRepository<Installment>
{
    public Task Register(List<Installment> entities, CancellationToken cancellationToken);
    public void Update(Installment entity, CancellationToken cancellationToken);
    public Task<Installment?> GetById(Guid id, CancellationToken cancellationToken);
    public void Delete(Installment entity, CancellationToken cancellationToken);
}
