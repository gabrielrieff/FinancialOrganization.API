
using FinancialOrganization.API.Communication.Response.Movement;
using FinancialOrganization.API.Domain.Repositories.Movements;

namespace FinancialOrganization.API.Application.UseCase.Movements.ListAll;

public class GetByDateRangeMovementsUseCase : IGetByDateRangeMovementsUseCase
{
    private readonly IMovementRepository _movementRepo;

    public GetByDateRangeMovementsUseCase(IMovementRepository movementRepo)
    {
        _movementRepo = movementRepo;
    }

    public async Task<List<MovementJson>> Execute(DateTime initialDate, DateTime endDate)
    {
        var result = await _movementRepo.GetByDateRange(initialDate, endDate);

        var mappedResult = result.Select(movement => new MovementJson
        {
            Id = movement.Id,
            Type = movement.Type,
            AmountTotal = movement.AmountTotal,
            Description = movement.Description,
            Category = movement.Category,
            Status = movement.Status,
            Card = movement.Card is not null ? new CardJson { Id = movement.Card.Id, Name = movement.Card.Name } : null,
            InstallmentPlan = new InstallmentPlanJson
            {
                Id = movement.InstallmentPlan.Id,
                InitialDate = movement.InstallmentPlan.InitialDate,
                FinalDate = movement.InstallmentPlan.FinalDate,
                TotalInstallments = movement.InstallmentPlan.TotalInstallment,
                Installments = movement.InstallmentPlan.Installments.Select(i => new InstallmentJson
                {
                    Id = i.Id,
                    Amount = i.Amount,
                    DueDate = i.DueDate,
                    Status = i.Status,
                    InstallmentNumber = i.InstallmentNumber
                }).ToList()
            },
        }).ToList();

        return mappedResult;
    }
}
