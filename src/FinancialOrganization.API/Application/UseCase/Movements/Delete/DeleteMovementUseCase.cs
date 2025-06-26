using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Movements.Delete;

public class DeleteMovementUseCase : IDeleteMovementUseCase
{
    private readonly IMovementRepository _movementRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteMovementUseCase(IMovementRepository movementRepo, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
    {
        _movementRepo = movementRepo;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid id, CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        var movement = await _movementRepo.GetById(user, id, cancellationToken);

        if (movement is null)
        {
            throw new NotFoundException($"Movement with ID {id} not found.");
        }

        _movementRepo.Delete(movement, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
    }
}
