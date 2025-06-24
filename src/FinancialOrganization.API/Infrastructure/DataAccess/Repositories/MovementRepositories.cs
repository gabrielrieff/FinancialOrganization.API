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

    public async Task<Movement> Get()
    {
       return await _dbContext.Movements
            .AsNoTracking()
            .Include(p => p.InstallmentPlan)
                .ThenInclude(i => i.Installments)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse("79d504b6-ed9b-4721-b69e-0d7edd169ebf"))
            ;
    }

    public async Task<List<Movement>> GetByDateRange(DateTime initialDate, DateTime endDate)
    {
        var startDate = new DateTime(initialDate.Year, initialDate.Month, 1);

        var dayFinalDate = DateTime.DaysInMonth(endDate.Year, endDate.Month);

        var finalDate = new DateTime(endDate.Year, endDate.Month, dayFinalDate);

        var result = await _dbContext.Movements
            .AsNoTracking()
            .Where(m =>
                m.InstallmentPlan.InitialDate <= finalDate &&
                m.InstallmentPlan.FinalDate >= startDate)
            .Include(c => c.Card)
            .Include(p => p.InstallmentPlan)
                .ThenInclude(i => i.Installments)
            .ToListAsync();

        return result;
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

    public async Task<SearchOutput<MovementDto>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;

        var startDate = input.SearchDate.HasValue
            ? new DateTime(input.SearchDate.Value.Year, input.SearchDate.Value.Month, 1)
            : (DateTime?)null;

        var endDate = startDate?.AddMonths(1).AddDays(-1);

        var query = _dbContext.Movements
            .AsNoTracking()
            .Where(m =>
                (string.IsNullOrWhiteSpace(input.Search) || m.Description.Contains(input.Search)) &&
                (
                    !startDate.HasValue ||
                    (
                        m.InstallmentPlan.Installments.Any(i => i.DueDate >= startDate && i.DueDate <= endDate)
                        || (m.InstallmentPlan.InitialDate <= endDate && m.InstallmentPlan.FinalDate >= startDate)
                    )
                )
            )
            .Select(m => new MovementDto
            {
                Id = m.Id,
                Description = m.Description,
                AmountTotal = m.AmountTotal,
                Type = m.Type,
                Category = m.Category,
                CardID = m.CardID,
                Status = m.Status,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt,
                Card = new CardDto
                {
                    Id = m.Card.Id,
                    Name = m.Card.Name,
                },
                InstallmentPlan = new InstallmentPlanDto
                {
                    FinalDate = m.InstallmentPlan.FinalDate,
                    InitialDate = m.InstallmentPlan.InitialDate,
                    TotalInstallments = m.InstallmentPlan.TotalInstallment,
                    Installments = m.InstallmentPlan.Installments
                        .Where(i => !startDate.HasValue || (i.DueDate >= startDate && i.DueDate <= endDate))
                        .Select(i => new InstallmentDto
                        {
                            Id = i.Id,
                            InstallmentNumber = i.InstallmentNumber,
                            DueDate = i.DueDate,
                            Amount = i.Amount,
                            Status = i.Status
                        }).ToList()
                }
            });

        query = AddOrderToQuery(query, input.OrderBy, input.Order);

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync(cancellationToken);

        return new SearchOutput<MovementDto>(input.Page, input.PerPage, total, items);
    }

    public void Update(Movement entity, CancellationToken cancellationToken)
    {
        _dbContext.Movements.Update(entity);
    }

    private IQueryable<MovementDto> AddOrderToQuery(
        IQueryable<MovementDto> query,
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
