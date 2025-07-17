using FinancialOrganization.API.Communication.DTOs;

namespace FinancialOrganization.API.Application.UseCase.Movements.ListAll;

public interface IGetByDateRangeMovementsUseCase
{
    public Task<List<MovementDto>> Execute(DateTime initialDate, DateTime endDate);
}
