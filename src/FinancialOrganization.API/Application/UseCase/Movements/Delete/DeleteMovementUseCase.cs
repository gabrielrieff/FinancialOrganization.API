using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Movements.Delete;

public class DeleteMovementUseCase : IDeleteMovementUseCase
{
    private readonly IMovementRepository _movementRepo;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMovementUseCase(IMovementRepository movementRepo, IUnitOfWork unitOfWork)
    {
        _movementRepo = movementRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid id, CancellationToken cancellationToken)
    {
        var movement = await _movementRepo.GetById(id, cancellationToken);

        if (movement is null)
        {
            throw new NotFoundException($"Movement with ID {id} not found.");
        }

        _movementRepo.Delete(movement, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
    }
}
