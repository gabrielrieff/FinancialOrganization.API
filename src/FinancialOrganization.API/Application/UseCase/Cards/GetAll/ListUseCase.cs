using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Application.UseCase.Cards.GetAll;

public class ListUseCase : IListUseCase
{
    //private readonly ICardRepository _repository;

    //public ListUseCase(ICardRepository repository)
    //{
    //    _repository = repository;
    //}

    public async Task<SearchOutput<RegisterCardResponse>> Execute(SearchListRequest request, CancellationToken cancellationToken)
    {
        //var useId = Guid.NewGuid();
        //var search = new SearchInput(
        //    page: request.Page,
        //    perPage: request.PerPage,
        //    search: request.Search,
        //    orderBy: request.Sort,
        //    order: request.Dir
        //);
        //var result = await _repository.Search(search, cancellationToken);

        //return new SearchOutput<RegisterCardResponse>(
        //    currentPage: result.CurrentPage,
        //    perPage: result.PerPage,
        //    total: result.Total,
        //    items: result.Items.Select(card => new RegisterCardResponse(card.Id, card.Name)).ToList()
        //);
        return new SearchOutput<RegisterCardResponse>(
            currentPage: 1,
            perPage: 10,
            total: 0,
            items: new List<RegisterCardResponse>()
        );
    }
}
