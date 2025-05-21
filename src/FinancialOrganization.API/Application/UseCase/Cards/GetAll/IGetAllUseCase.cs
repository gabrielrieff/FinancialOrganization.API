using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Application.UseCase.Cards.GetAll;

public interface IGetAllUseCase
{
    Task<AllCardResponse> Execute(CancellationToken cancellationToken);
}
