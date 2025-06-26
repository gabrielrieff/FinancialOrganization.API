using FinancialOrganization.API.Communication.Request.Moviment;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Exception.ExceptionsBase;
using FinancialOrganization.API.Domain.Services.LoggedUser;

namespace FinancialOrganization.API.Application.UseCase.Movements.Update;

public class UpdateMovementUseCase : IUpdateMovementUseCase
{
    private readonly IMovementRepository _movementRepository;
    private readonly IInstallmentPlanRepository _installmentPlanRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public UpdateMovementUseCase(IMovementRepository movementRepository, 
        IInstallmentPlanRepository installmentPlanRepository, 
        IUnitOfWork unitOfWork, 
        ILoggedUser loggedUser)
    {
        _movementRepository = movementRepository;
        _installmentPlanRepository = installmentPlanRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid movementId, UpdateMovementJson request, CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var movement = await _movementRepository.GetById(user, movementId, cancellationToken);

        if (movement is null)
        {
            throw new NotFoundException($"Movement with ID {movementId} not found.");
        }

        if(request.CardId is not null)
        {
            movement.UpdateCard(request.CardId.Value);
            movement.InstallmentPlan.UpdateCard(request.CardId.Value);
            _installmentPlanRepository.Update(movement.InstallmentPlan, cancellationToken);
        }

        if (request.CategoryType is not null)
            movement.UpdateCategory(request.CategoryType.Value);
        if (request.Description is not null)
            movement.UpdateDescription(request.Description);

        _movementRepository.Update(movement, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

    }
}
