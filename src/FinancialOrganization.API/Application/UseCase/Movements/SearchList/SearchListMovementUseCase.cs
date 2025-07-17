using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Communication.Request;
using FinancialOrganization.API.Communication.Response.Movement;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using MapsterMapper;

namespace FinancialOrganization.API.Application.UseCase.Movements.SearchList;

public class SearchListMovementUseCase : ISearchListMovementUseCase
{
    private readonly IMovementRepository _movementRepo;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public SearchListMovementUseCase(IMovementRepository movementRepo, ILoggedUser loggedUser, IMapper mapper)
    {
        _movementRepo = movementRepo;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<SearchOutput<MovementDto>> Execute(SearchListRequest request, CancellationToken cancellationToken)
    {
        var search = new SearchInput(
            page: request.Page,
            perPage: request.PerPage,
            search: request.Search,
            searchDate: request.SearchDate,
            orderBy: request.Sort,
            order: request.Dir
        );
        var user = await _loggedUser.Get();
        var result = await _movementRepo.Search(user, search, cancellationToken);

        return new SearchOutput<MovementDto>(
            currentPage: result.CurrentPage,
            perPage: result.PerPage,
            total: result.Total,
            items: result.Items.Select(x => _mapper.Map<MovementDto>(x)).ToList()
        );
    }
}
