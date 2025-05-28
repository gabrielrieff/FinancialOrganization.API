
using FinancialOrganization.API.Communication.Request.Moviment;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Enums;

namespace FinancialOrganization.API.Application.UseCase.Movements.Register;

public class RegisterMovementUseCase : IRegisterMovementUseCase
{
    public Task Execute(MovimentRegisterJson request, CancellationToken cancellationToken)
    {
        var movement = new Movement(
                        request.Type,
                        request.AmountTotal,
                        request.Description,
                        request.Category,
                        request.CardID,
                        status: request.Status);

        var installmentPlan = new InstallmentPlan(
            totalInstallment: request.Installments,
            initialDate: request.InitialDate,
            movementId: movement.Id,
            cardID: request.CardID
            );

        var installments = new List<Installment>();
        for (int i = 0; i < installmentPlan.TotalInstallment; i++)
        {
            installments.Add(new Installment(
                installmentNumber: i + 1,
                status: Status.Paid,
                amount: request.AmountTotal / installmentPlan.TotalInstallment,
                dueDate: installmentPlan.InitialDate.AddMonths(i),
                installmentPlanId: installmentPlan.Id
                ));
        }

    }
}
