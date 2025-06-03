
using FinancialOrganization.API.Communication.Response.Movement;
using FinancialOrganization.API.Domain.Repositories.Movements;

namespace FinancialOrganization.API.Application.UseCase.Movements.ListAll;

public class ListAllMovementsUseCase : IListAllMovementsUseCase
{
    private readonly IMovementRepository _movementRepo;

    public ListAllMovementsUseCase(IMovementRepository movementRepo)
    {
        _movementRepo = movementRepo;
    }

    public async Task<MovementJson> Execute()
    {
        var result = await _movementRepo.Get();

        return new MovementJson
        {
            AmountTotal = result.AmountTotal,
            CardID = result.CardID,
            Category = result.Category,
            Description = result.Description,
            Type = result.Type,
            Status = result.Status,
            InstallmentPlan = new InstallmentPlanJson
            {
                FinalDate = result.InstallmentPlan!.FinalDate,
                InitialDate = result.InstallmentPlan.InitialDate,
                Installments = result.InstallmentPlan.Installments.Select(installment => new InstallmentJson
                {
                    InstallmentNumber = installment.InstallmentNumber,
                    Status = installment.Status,
                    Amount = installment.Amount,
                    DueDate = installment.DueDate
                }).ToList()
            }
        };
    }
}
