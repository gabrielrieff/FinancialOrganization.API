using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

public interface ISearchableRepository<T>
{
    Task<SearchOutput<T>> Search(User user, SearchInput input, CancellationToken cancellationToken);
}
