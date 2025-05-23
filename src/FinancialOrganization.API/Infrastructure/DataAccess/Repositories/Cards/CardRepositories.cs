using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;
using FinancialOrganization.API.Infrasctructure.DataAccess;

namespace FinancialOrganization.API.Infrastructure.DataAccess.Repositories.Cards;

public class CardRepositories : ICardRepository
{
    private readonly FinancialOrganizationDbContext _context;

    public CardRepositories(FinancialOrganizationDbContext context)
    {
        _context = context;
    }

    public Task Delete(Card entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Card>> GetAll(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Card> GetById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Register(Card entity, CancellationToken cancellationToken)
    {
        await _context.Cards.AddAsync(entity);
    }

    public Task<SearchOutput<Card>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Card entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
