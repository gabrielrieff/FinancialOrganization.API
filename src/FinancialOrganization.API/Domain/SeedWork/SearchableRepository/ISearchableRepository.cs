namespace FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

public interface ISearchableRepository<T>
{
    Task<SearchOutput<T>> Search(SearchInput input, CancellationToken cancellationToken);
}
