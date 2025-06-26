using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Services.LoggedUser;

namespace FinancialOrganization.API.Application.UseCase.Cards.ListAll;

public class ListAllUseCase : IListAllUseCase
{
    private readonly ICardRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public ListAllUseCase(ICardRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ListAllResponse> Execute(CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var result = await _repository.GetAll(user, cancellationToken);

        return new ListAllResponse
        {
            Items = result.Select(x => new RegisterCardResponse(x.Id, x.Name)).ToList()
        };
    }
}
