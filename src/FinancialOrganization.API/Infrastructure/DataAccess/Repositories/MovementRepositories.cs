using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Enums;
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
        _dbContext.Movements.Remove(entity);
    }

    public async Task<Movement?> Get(User user, Guid movementId)
    {
       return await _dbContext.Movements
            .AsNoTracking()
            .Include(p => p.InstallmentPlan)
                .ThenInclude(i => i.Installments)
            .FirstOrDefaultAsync(x => x.Id == movementId && x.UserId == user.Id);
    }

    public async Task<List<Movement>> GetByDateRange(User user, DateTime initialDate, DateTime endDate)
    {
        var startDate = new DateTime(initialDate.Year, initialDate.Month, 1);

        var dayFinalDate = DateTime.DaysInMonth(endDate.Year, endDate.Month);

        var finalDate = new DateTime(endDate.Year, endDate.Month, dayFinalDate);

        var result = await _dbContext.Movements
            .AsNoTracking()
            .Where(m =>
                m.UserId == user.Id &&
                m.InstallmentPlan.InitialDate <= finalDate &&
                m.InstallmentPlan.FinalDate >= startDate)
            .Include(c => c.Card)
            .Include(p => p.InstallmentPlan)
                .ThenInclude(i => i.Installments.Where(
                    i => 
                    i.DueDate <= finalDate &&
                    i.DueDate >= startDate))
            .ToListAsync();

        return result;
    }

    public async Task<Movement?> GetById(User user, Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Movements
            .AsNoTracking()
            .Include(p => p.InstallmentPlan)
                .ThenInclude(i => i.Installments)
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id, cancellationToken: cancellationToken);
    }

    public async Task Register(Movement entity, CancellationToken cancellationToken)
    {
        await _dbContext.Movements.AddAsync(entity, cancellationToken);
    }

    public async Task<SearchOutput<Movement>> Search(User user, SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;

        var startDate = input.SearchDate.HasValue
            ? new DateTime(input.SearchDate.Value.Year, input.SearchDate.Value.Month, 1)
            : (DateTime?)null;

        var endDate = startDate?.AddMonths(1).AddDays(-1);

        var query = _dbContext.Movements
            .AsNoTracking()
            .Include(m => m.InstallmentPlan)
                .ThenInclude(p => p.Installments)
            .Where(m =>
                m.UserId == user.Id &&
                (
                    !startDate.HasValue ||
                    (
                        m.InstallmentPlan.Installments.Any(i => i.DueDate >= startDate && i.DueDate <= endDate)
                        || (m.InstallmentPlan.InitialDate <= endDate && m.InstallmentPlan.FinalDate >= startDate)
                    )
                )
            );

        query = AddOrderToQuery(query, input.OrderBy, input.Order);

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync(cancellationToken);

        return new SearchOutput<Movement>(input.Page, input.PerPage, total, items);
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
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Description)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}
