using FinancialOrganization.API.Communication.Request.Moviment;

namespace FinancialOrganization.API.Application.UseCase.Movements.Update;

public interface IUpdateMovementUseCase
{
    Task Execute(Guid movementId, UpdateMovementJson request, CancellationToken cancellationToken);
}
