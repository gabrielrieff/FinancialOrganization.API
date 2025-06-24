using FinancialOrganization.API.Communication.Response.Movement;

namespace FinancialOrganization.API.Application.UseCase.Movements.ListAll;

public interface IGetByDateRangeMovementsUseCase
{
    public Task<List<MovementJson>> Execute(DateTime initialDate, DateTime endDate);
}
