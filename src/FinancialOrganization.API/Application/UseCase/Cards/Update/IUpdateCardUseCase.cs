using FinancialOrganization.API.Communication.Request.Cards;

namespace FinancialOrganization.API.Application.UseCase.Cards.Update;

public interface IUpdateCardUseCase
{
    Task Execute(UpdateCardRequest request, Guid cardId, CancellationToken cancellationToken);
}
