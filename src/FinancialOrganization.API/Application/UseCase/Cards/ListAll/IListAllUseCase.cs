using FinancialOrganization.API.Communication.Response.Cards;

namespace FinancialOrganization.API.Application.UseCase.Cards.ListAll;

public interface IListAllUseCase
{
    Task<ListAllResponse> Execute(CancellationToken cancellationToken);
}
