using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using MapsterMapper;

namespace FinancialOrganization.API.Application.UseCase.Movements.ListAll;

public class GetByDateRangeMovementsUseCase : IGetByDateRangeMovementsUseCase
{
    private readonly IMovementRepository _movementRepo;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetByDateRangeMovementsUseCase(IMovementRepository movementRepo, ILoggedUser loggedUser, IMapper mapper)
    {
        _movementRepo = movementRepo;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<List<MovementDto>> Execute(DateTime initialDate, DateTime endDate)
    {
        var user = await _loggedUser.Get();
        var result = await _movementRepo.GetByDateRange(user, initialDate, endDate);
        return result.Select(x => _mapper.Map<MovementDto>(x)).ToList();
    }
}
