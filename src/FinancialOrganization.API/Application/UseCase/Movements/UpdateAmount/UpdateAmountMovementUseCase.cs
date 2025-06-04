
using FinancialOrganization.API.Communication.Request.Moviment;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Movements.UpdateAmount;

public class UpdateAmountMovementUseCase : IUpdateAmountMovementUseCase
{
    private readonly IMovementRepository _movementRepository;
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAmountMovementUseCase(
        IMovementRepository movementRepository, 
        IInstallmentRepository installmentRepository, 
        IUnitOfWork unitOfWork)
    {
        _movementRepository = movementRepository;
        _installmentRepository = installmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid movementId, UpdateMovementAmountJson request, CancellationToken cancellationToken)
    {
        var movement = await _movementRepository.GetById(movementId, cancellationToken);

        if(movement is null)
        {
            throw new NotFoundException($"Movement with ID {movementId} not found.");
        }

        movement.UpdateAmount(request.Amount);

        foreach (var item in movement.InstallmentPlan.Installments)
        {
            var resultAmount = request.Amount / movement.InstallmentPlan.TotalInstallment;
            item.UpdateAmount(resultAmount);
        }

        _movementRepository.Update(movement, cancellationToken);
        _installmentRepository.Update((List<Installment>)movement.InstallmentPlan.Installments, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
    }
}
