using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Communication.Request.Moviment;

namespace FinancialOrganization.API.Application.UseCase.Movements.Register;

public interface IRegisterMovementUseCase
{
    Task<MovementDto> Execute(MovimentRegisterJson request, CancellationToken cancellationToken);
}
