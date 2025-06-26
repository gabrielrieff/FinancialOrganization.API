using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.Repositories;

public interface IGenegicRepository<TAggregate> : IRepository
{
    public Task Register(TAggregate entity, CancellationToken cancellationToken);
    public void Update(TAggregate entity, CancellationToken cancellationToken);
    public Task<TAggregate?> GetById(User user, Guid id, CancellationToken cancellationToken);
    public void Delete(TAggregate entity, CancellationToken cancellationToken);

}
