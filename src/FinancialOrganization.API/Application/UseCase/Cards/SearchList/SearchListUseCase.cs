using FinancialOrganization.API.Communication.Request;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Application.UseCase.Cards.GetAll;

public class SearchListUseCase : ISearchListUseCase
{
    private readonly ICardRepository _repository;

    public SearchListUseCase(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<SearchOutput<RegisterCardResponse>> Execute(SearchListRequest request, CancellationToken cancellationToken)
    {
        var useId = Guid.NewGuid();
        var search = new SearchInput(
            page: request.Page,
            perPage: request.PerPage,
            search: request.Search,
            orderBy: request.Sort,
            order: request.Dir
        );
        var result = await _repository.Search(search, cancellationToken);

        return new SearchOutput<RegisterCardResponse>(
            currentPage: result.CurrentPage,
            perPage: result.PerPage,
            total: result.Total,
            items: result.Items.Select(card => new RegisterCardResponse(card.Id, card.Name)).ToList()
        );
    }
}
