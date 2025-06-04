using FinancialOrganization.API.Communication.Request;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Application.UseCase.Cards.GetAll;

public interface ISearchListUseCase
{
    Task<SearchOutput<RegisterCardResponse>> Execute(SearchListRequest request, CancellationToken cancellationToken);
}
