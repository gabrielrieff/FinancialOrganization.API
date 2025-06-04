using FinancialOrganization.API.Communication.Request;
using FinancialOrganization.API.Communication.Response.Movement;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Application.UseCase.Movements.SearchList;

public class SearchListMovementUseCase : ISearchListMovementUseCase
{
    private readonly IMovementRepository _movementRepo;

    public SearchListMovementUseCase(IMovementRepository movementRepo)
    {
        _movementRepo = movementRepo;
    }

    public async Task<SearchOutput<MovementJson>> Execute(SearchListRequest request, CancellationToken cancellationToken)
    {
        var search = new SearchInput(
            page: request.Page,
            perPage: request.PerPage,
            search: request.Search,
            searchDate: request.SearchDate,
            orderBy: request.Sort,
            order: request.Dir
        );

        var result = await _movementRepo.Search(search, cancellationToken);

        return new SearchOutput<MovementJson>(
            currentPage: result.CurrentPage,
            perPage: result.PerPage,
            total: result.Total,
            items: result.Items.Select(movement => new MovementJson
            {
                Id = movement.Id,
                AmountTotal = movement.AmountTotal,
                CardID = movement.CardID,
                Category = movement.Category,
                Description = movement.Description,
                Type = movement.Type,
                Status = movement.Status,
                InstallmentPlan = new InstallmentPlanJson
                {
                    Id = movement.InstallmentPlan.Id,
                    FinalDate = movement.InstallmentPlan!.FinalDate,
                    InitialDate = movement.InstallmentPlan!.InitialDate,
                    Installments = movement.InstallmentPlan!.Installments.Select(installment => new InstallmentJson
                    {
                        Id = installment.Id,
                        InstallmentNumber = installment.InstallmentNumber,
                        Status = installment.Status,
                        Amount = installment.Amount,
                        DueDate = installment.DueDate
                    }).ToList()
                }
            }).ToList()
        );
    }
}
