using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Domain.Repositories.Cards;

public interface ICardRepository : IGenegicRepository<Card>, ISearchableRepository<Card>
{
    Task<IList<Card>> GetAll(User user, CancellationToken cancellationToken);
}
