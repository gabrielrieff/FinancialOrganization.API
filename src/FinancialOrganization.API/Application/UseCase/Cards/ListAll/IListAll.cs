using FinancialOrganization.API.Communication.Response.Cards;

namespace FinancialOrganization.API.Application.UseCase.Cards.ListAll;

public interface IListAll
{
    Task<ListAllResponse> Execute(CancellationToken cancellationToken);
}
