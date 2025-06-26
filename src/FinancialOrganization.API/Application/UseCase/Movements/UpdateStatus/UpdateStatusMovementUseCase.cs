using FinancialOrganization.API.Communication.Request.Moviment;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Movements.UpdateStatus;

public class UpdateStatusMovementUseCase : IUpdateStatusMovementUseCase
{
    private readonly IMovementRepository _movementRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public UpdateStatusMovementUseCase(IMovementRepository movementRepository, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _movementRepository = movementRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid movementId, UpdateMovementStatusJson request, CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var movement = await _movementRepository.GetById(user, movementId, cancellationToken);

        if (movement is null)
        {
            throw new NotFoundException($"Movement with ID {movementId} not found.");
        }

        movement.SetStatus(request.Status);

        _movementRepository.Update(movement, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
    }
}
