namespace FinancialOrganization.API.Application.UseCase.Movements.Delete;

public interface IDeleteMovementUseCase
{
    Task Execute(Guid id, CancellationToken cancellationToken);
}
