using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Cards.Delete;

public class DeleteCardUseCase : IDeleteCardUseCase
{
    private readonly ICardRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCardUseCase(ICardRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid cardId, CancellationToken cancellationToken)
    {
        var card = await _repository.GetById(cardId, cancellationToken);

        if (card is null)
        {
            throw new NotFoundException("Not found Card");
        }

        _repository.Delete(card, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}
