using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Cards;

namespace FinancialOrganization.API.Application.UseCase.Cards.Register;

public class RegisterCardUseCase : IRegisterCardUseCase
{
    private readonly ICardRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCardUseCase(ICardRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterCardResponse> Execute(RegisterCardRequest request, CancellationToken cancellationToken)
    {
        
        var card = new Card(request.Name);
        await _repository.Register(card, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return new RegisterCardResponse(card.Id, card.Name);
    }
}
