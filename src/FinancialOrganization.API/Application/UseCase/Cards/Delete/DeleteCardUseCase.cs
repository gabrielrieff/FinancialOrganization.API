using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Exception.ExceptionsBase;
using FinancialOrganization.API.Domain.Services.LoggedUser;

namespace FinancialOrganization.API.Application.UseCase.Cards.Delete;

public class DeleteCardUseCase : IDeleteCardUseCase
{
    private readonly ICardRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteCardUseCase(ICardRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid cardId, CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var card = await _repository.GetById(user, cardId, cancellationToken);

        if (card is null)
        {
            throw new NotFoundException("Not found Card");
        }

        _repository.Delete(card, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}
