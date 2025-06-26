using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Cards.Update;

public class UpdateCardUseCase : IUpdateCardUseCase
{
    private readonly ICardRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public UpdateCardUseCase(ICardRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(UpdateCardRequest request, Guid cardId, CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var card = await _repository.GetById(user, cardId, cancellationToken);

        if (card is null)
        {
            throw new NotFoundException("Not found Card");
        }

        card.UpdateName(request.Name);

        _repository.Update(card, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}
