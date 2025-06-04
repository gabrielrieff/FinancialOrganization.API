using FinancialOrganization.API.Communication.Request.Moviment;

namespace FinancialOrganization.API.Application.UseCase.Movements.UpdateAmount;

public interface IUpdateAmountMovementUseCase
{
    Task Execute(Guid movementId, UpdateMovementAmountJson request, CancellationToken cancellationToken);
}
