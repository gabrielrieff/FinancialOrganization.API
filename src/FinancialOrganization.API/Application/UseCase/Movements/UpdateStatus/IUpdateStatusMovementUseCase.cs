using FinancialOrganization.API.Communication.Request.Moviment;

namespace FinancialOrganization.API.Application.UseCase.Movements.UpdateStatus;

public interface IUpdateStatusMovementUseCase
{
    Task Execute(Guid movementId, UpdateMovementStatusJson request, CancellationToken cancellationToken);
}
