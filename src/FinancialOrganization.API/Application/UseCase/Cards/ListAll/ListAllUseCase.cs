using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Repositories.Cards;

namespace FinancialOrganization.API.Application.UseCase.Cards.ListAll;

public class ListAllUseCase : IListAllUseCase
{
    private readonly ICardRepository _repository;

    public ListAllUseCase(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListAllResponse> Execute(CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll(Guid.NewGuid(), cancellationToken);

        return new ListAllResponse
        {
            Items = result.Select(x => new RegisterCardResponse(x.Id, x.Name)).ToList()
        };
    }
}
