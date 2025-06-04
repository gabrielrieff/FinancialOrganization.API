using FinancialOrganization.API.Communication.Request.Installment;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Installments.UpdateStatus;

public class UpdateStatusInstallmentUseCase : IUpdateStatusInstallmentUseCase
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStatusInstallmentUseCase(IInstallmentRepository installmentRepository, IUnitOfWork unitOfWork)
    {
        _installmentRepository = installmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid instalmentId, UpdateStatusInstallmentJson request, CancellationToken cancellationToken)
    {
        var installment = await _installmentRepository.GetById(instalmentId, cancellationToken);

        if(installment is null)
        {
            throw new NotFoundException($"Installment with ID {instalmentId} not found.");
        }

        installment.UpdateStatus(request.Status);

        _installmentRepository.Update(new List<Installment> { installment }, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
    }
}
