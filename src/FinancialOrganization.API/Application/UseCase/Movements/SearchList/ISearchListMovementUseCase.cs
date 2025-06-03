using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Communication.Response.Movement;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Application.UseCase.Movements.SearchList;

public interface ISearchListMovementUseCase
{
    Task<SearchOutput<MovementJson>> Execute(SearchListRequest request, CancellationToken cancellationToken);
}
