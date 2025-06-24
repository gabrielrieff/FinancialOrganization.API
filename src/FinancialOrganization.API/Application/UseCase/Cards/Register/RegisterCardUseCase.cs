using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Services.LoggedUser;

namespace FinancialOrganization.API.Application.UseCase.Cards.Register;

public class RegisterCardUseCase : IRegisterCardUseCase
{
    private readonly ICardRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public RegisterCardUseCase(ICardRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task<RegisterCardResponse> Execute(RegisterCardRequest request, CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var card = new Card(request.Name, user.Id);
        await _repository.Register(card, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return new RegisterCardResponse(card.Id, card.Name);
    }
}
