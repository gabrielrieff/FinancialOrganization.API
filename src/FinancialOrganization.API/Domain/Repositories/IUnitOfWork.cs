namespace FinancialOrganization.API.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
}
