using FinancialOrganization.API.Communication.Request;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;
using FinancialOrganization.API.Domain.Services.LoggedUser;

namespace FinancialOrganization.API.Application.UseCase.Cards.GetAll;

public class SearchListUseCase : ISearchListUseCase
{
    private readonly ICardRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public SearchListUseCase(ICardRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<SearchOutput<RegisterCardResponse>> Execute(SearchListRequest request, CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var search = new SearchInput(
            page: request.Page,
            perPage: request.PerPage,
            search: request.Search,
            searchDate: request.SearchDate,
            orderBy: request.Sort,
            order: request.Dir
        );
        var result = await _repository.Search(user, search, cancellationToken);

        return new SearchOutput<RegisterCardResponse>(
            currentPage: result.CurrentPage,
            perPage: result.PerPage,
            total: result.Total,
            items: result.Items.Select(card => new RegisterCardResponse(card.Id, card.Name)).ToList()
        );
    }
}
