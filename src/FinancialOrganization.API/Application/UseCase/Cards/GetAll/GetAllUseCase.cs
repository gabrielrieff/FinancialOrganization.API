using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Repositories.Cards;

namespace FinancialOrganization.API.Application.UseCase.Cards.GetAll;

public class GetAllUseCase : IGetAllUseCase
{
    private readonly ICardRepository _repository;

    public GetAllUseCase(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<AllCardResponse> Execute(CancellationToken cancellationToken)
    {
        var useId = Guid.NewGuid();
        var result = await _repository.GetAll(useId, cancellationToken);

        return new AllCardResponse
        {
            Cards = result.Select(card => new RegisterCardResponse(card.Id, card.Name)).ToList()
        };
    }
}
