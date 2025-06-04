using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Enums;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;
using FinancialOrganization.API.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

    public async Task<Movement?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Movements
            .AsNoTracking()
            .Include(p => p.InstallmentPlan)
                .ThenInclude(i => i.Installments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Register(Movement entity, CancellationToken cancellationToken)
    {
        await _dbContext.Movements.AddAsync(entity, cancellationToken);
    }

    public async Task<SearchOutput<Movement>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        IQueryable<Movement> query = _dbContext.Movements
            .AsNoTracking()
            .Include(p => p.InstallmentPlan)
                .ThenInclude(i => i.Installments);

        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if (!string.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Description.Contains(input.Search));
        var total = await query.CountAsync();
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        return new(input.Page, input.PerPage, total, items);
    }

    public void Update(Movement entity, CancellationToken cancellationToken)
    {
        _dbContext.Movements.Update(entity);
    }

    private IQueryable<Movement> AddOrderToQuery(
        IQueryable<Movement> query,
        string orderProperty,
        SearchOrder order
    )
    {
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Description)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Description)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Description)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}
