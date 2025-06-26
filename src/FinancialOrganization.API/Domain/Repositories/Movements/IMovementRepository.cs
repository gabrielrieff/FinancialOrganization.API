using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Domain.Repositories.Movements;

public interface IMovementRepository : IGenegicRepository<Movement>, ISearchableRepository<MovementDto>
{
    public Task<Movement?> Get(User user, Guid movementId);
    public Task<List<Movement>> GetByDateRange(User user, DateTime initialDate, DateTime endDate);
}
