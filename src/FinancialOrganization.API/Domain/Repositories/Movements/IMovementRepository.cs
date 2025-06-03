using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Domain.Repositories.Movements;

public interface IMovementRepository : IGenegicRepository<Movement>, ISearchableRepository<Movement>
{
    public Task<Movement> Get();

}
