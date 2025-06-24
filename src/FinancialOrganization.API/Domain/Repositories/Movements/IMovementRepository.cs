using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Domain.Repositories.Movements;

public interface IMovementRepository : IGenegicRepository<Movement>, ISearchableRepository<MovementDto>
{
    public Task<Movement> Get();
    public Task<List<Movement>> GetByDateRange(DateTime initialDate, DateTime endDate);
}
