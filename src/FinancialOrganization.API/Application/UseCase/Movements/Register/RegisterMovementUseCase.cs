
using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Communication.Request.Moviment;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Enums;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using MapsterMapper;

namespace FinancialOrganization.API.Application.UseCase.Movements.Register;

public class RegisterMovementUseCase : IRegisterMovementUseCase
{
    private readonly IMovementRepository _movementRepo;
    private readonly IInstallmentPlanRepository _installmentPlanRepo;
    private readonly IInstallmentRepository _installmentRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public RegisterMovementUseCase(IMovementRepository movementRepo, 
        IInstallmentPlanRepository installmentPlanRepo, 
        IInstallmentRepository installmentRepo, 
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        ILoggedUser loggedUser)
    {
        _movementRepo = movementRepo;
        _installmentPlanRepo = installmentPlanRepo;
        _installmentRepo = installmentRepo;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<MovementDto> Execute(MovimentRegisterJson request, CancellationToken cancellationToken)
    {
        var user = await _loggedUser.Get();
        
        var movement = new Movement(
                        request.Type,
                        request.AmountTotal,
                        request.Description,
                        request.Category,
                        user.Id,
                        request.CardID,
                        status: request.Status ?? Status.Waiting);

        var installmentPlan = new InstallmentPlan(
            totalInstallment: request.Installments,
            initialDate: request.InitialDate,
            movementId: movement.Id,
            cardID: request.CardID
            );

        movement.SetInstallmentPlanId(installmentPlan.Id);

        var installments = new List<Installment>();
        for (int i = 0; i < installmentPlan.TotalInstallment; i++)
        {
            installments.Add(new Installment(
                installmentNumber: i + 1,
                status: Status.Waiting,
                amount: (request.AmountTotal / installmentPlan.TotalInstallment),
                dueDate: installmentPlan.InitialDate.AddMonths(i),
                installmentPlanId: installmentPlan.Id
                ));
        }

        await Task.WhenAll(
            _movementRepo.Register(movement, cancellationToken),
            _installmentPlanRepo.Register(installmentPlan, cancellationToken),
            _installmentRepo.Register(installments, cancellationToken)
        );
        
        await _unitOfWork.Commit(cancellationToken);

         movement.SetInstallmentPlan(installmentPlan);
         movement.InstallmentPlan.setInstalments(installments);

        var movementDto = _mapper.Map<MovementDto>(movement);

        return movementDto;
    }
}
