using FinancialOrganization.API.Communication.Request.Moviment;

namespace FinancialOrganization.API.Application.UseCase.Movements.Register;

public interface IRegisterMovementUseCase
{
    Task Execute(MovimentRegisterJson request, CancellationToken cancellationToken);
}
