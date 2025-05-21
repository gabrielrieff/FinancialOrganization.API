namespace FinancialOrganization.API.Domain.Repositories;

public interface IGenegicRepository<TAggregate> : IRepository
{
    public Task Register(TAggregate entity, CancellationToken cancellationToken);
    public Task Update(TAggregate entity, CancellationToken cancellationToken);
    public Task Delete(TAggregate entity, CancellationToken cancellationToken);
}
