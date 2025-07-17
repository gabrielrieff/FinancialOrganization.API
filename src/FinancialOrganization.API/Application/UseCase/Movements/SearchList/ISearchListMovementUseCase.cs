using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Communication.Request;
using FinancialOrganization.API.Communication.Response.Movement;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Application.UseCase.Movements.SearchList;

public interface ISearchListMovementUseCase
{
    Task<SearchOutput<MovementDto>> Execute(SearchListRequest request, CancellationToken cancellationToken);
}
