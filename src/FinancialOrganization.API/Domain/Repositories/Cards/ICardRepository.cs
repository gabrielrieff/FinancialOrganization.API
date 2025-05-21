using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.Repositories.Cards;

public interface ICardRepository : IGenegicRepository<Card>
{
    Task<Card> GetById(Guid cardId, CancellationToken cancellationToken);
    Task<IList<Card>> GetAll(Guid userId, CancellationToken cancellationToken);
}
