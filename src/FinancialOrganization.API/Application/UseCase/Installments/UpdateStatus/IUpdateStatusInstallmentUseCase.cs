using FinancialOrganization.API.Communication.Request.Installment;

namespace FinancialOrganization.API.Application.UseCase.Installments.UpdateStatus;

public interface IUpdateStatusInstallmentUseCase
{
    Task Execute(Guid instalmentId, UpdateStatusInstallmentJson request, CancellationToken cancellationToken);
}
