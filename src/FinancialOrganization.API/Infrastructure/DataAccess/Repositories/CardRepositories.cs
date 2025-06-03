using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Enums;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;
using FinancialOrganization.API.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Infrastructure.DataAccess.Repositories;

public class CardRepositories : ICardRepository
{
    private readonly FinancialOrganizationDbContext _dbContext;

    public CardRepositories(FinancialOrganizationDbContext context)
    {
        _dbContext = context;
    }

    public void Delete(Card entity, CancellationToken cancellationToken)
    {
        _dbContext.Cards.Remove(entity);
    }

    public async Task<IList<Card>> GetAll(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Cards.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Card?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Cards.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task Register(Card entity, CancellationToken cancellationToken)
    {
        await _dbContext.Cards.AddAsync(entity);
    }

    public async Task<SearchOutput<Card>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _dbContext.Cards.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if (!string.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.Contains(input.Search));
        var total = await query.CountAsync();
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        return new(input.Page, input.PerPage, total, items);
    }

    public void Update(Card entity, CancellationToken cancellationToken)
    {
        _dbContext.Cards.Update(entity);
    }


    private IQueryable<Card> AddOrderToQuery(
    IQueryable<Card> query,
    string orderProperty,
    SearchOrder order
)
    {
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}
