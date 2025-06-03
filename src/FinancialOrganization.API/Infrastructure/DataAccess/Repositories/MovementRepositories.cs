using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;
using FinancialOrganization.API.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Infrastructure.DataAccess.Repositories;

public class MovementRepositories : IMovementRepository
{
    private readonly FinancialOrganizationDbContext _dbContext;

    public MovementRepositories(FinancialOrganizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Delete(Movement entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Movement> Get()
    {
       return await _dbContext.Movements
            .AsNoTracking()
            .Include(p => p.InstallmentPlan)
                .ThenInclude(i => i.Installments)
            .FirstOrDefaultAsync(x => x.CardID == Guid.Parse("b68e67f8-b849-4b35-87f8-a34b76b0e808"))
            ;
    }

    public Task<Movement?> GetById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Register(Movement entity, CancellationToken cancellationToken)
    {
        await _dbContext.Movements.AddAsync(entity, cancellationToken);
    }

    public Task<SearchOutput<Movement>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Update(Movement entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
