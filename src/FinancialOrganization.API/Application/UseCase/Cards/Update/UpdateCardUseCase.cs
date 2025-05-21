using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Cards.Update;

public class UpdateCardUseCase : IUpdateCardUseCase
{
    private readonly ICardRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCardUseCase(ICardRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateCardRequest request, Guid cardId, CancellationToken cancellationToken)
    {
        var card = await _repository.GetById(cardId, cancellationToken);

        if(card is null)
        {
            throw new NotFoundException("Not found Card");
        }

        card.UpdateName(request.Name);

        await _repository.Update(card, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
    }
}
