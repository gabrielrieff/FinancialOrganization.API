using FinancialOrganization.API.Communication.Request.Moviment;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Movements.Update;

public class UpdateMovementUseCase : IUpdateMovementUseCase
{
    private readonly IMovementRepository _movementRepository;
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMovementUseCase(
        IMovementRepository movementRepository, 
        IInstallmentRepository installmentRepository, 
        IUnitOfWork unitOfWork)
    {
        _movementRepository = movementRepository;
        _installmentRepository = installmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid movementId, UpdateMovementJson request, CancellationToken cancellationToken)
    {
        var movement = await _movementRepository.GetById(movementId, cancellationToken);

        if (movement is null)
        {
            throw new NotFoundException($"Movement with ID {movementId} not found.");
        }

        if(request.CardId is not null)
            movement.UpdateCard(request.CardId.Value);
        if (request.CategoryType is not null)
            movement.UpdateCategory(request.CategoryType.Value);
        if (request.Description is not null)
            movement.UpdateDescription(request.Description);

        _movementRepository.Update(movement, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

    }
}
