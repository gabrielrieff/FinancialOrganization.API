using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using MapsterMapper;

namespace FinancialOrganization.API.Application.UseCase.Cards.ListAll;

public class ListAllUseCase : IListAllUseCase
{
    private readonly ICardRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public ListAllUseCase(ICardRepository repository, ILoggedUser loggedUser, IMapper mapper)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<AllCards> Execute(CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var result = await _repository.GetAll(user, cancellationToken);

        return new AllCards
        {
            Items = result.Select(x => _mapper.Map<CardDto>(x)).ToList()
        };
    }
}
