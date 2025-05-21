namespace FinancialOrganization.API.Application.UseCase.Cards.Delete;

public interface IDeleteCardUseCase
{
    Task Execute(Guid cardId, CancellationToken cancellationToken);
}
