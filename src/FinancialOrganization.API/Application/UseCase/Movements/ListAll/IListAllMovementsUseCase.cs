using FinancialOrganization.API.Communication.Response.Movement;
using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Application.UseCase.Movements.ListAll;

public interface IListAllMovementsUseCase
{
    public Task<ListAllMovementJson> Execute();
}
