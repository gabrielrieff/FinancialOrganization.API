using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.Repositories.Installments;

public interface IInstallmentRepository
{
    public Task Register(List<Installment> entities, CancellationToken cancellationToken);
    public void Update(List<Installment> entities, CancellationToken cancellationToken);
    public Task<Installment?> GetById(Guid id, CancellationToken cancellationToken);
    public void Delete(Installment entity, CancellationToken cancellationToken);
}
