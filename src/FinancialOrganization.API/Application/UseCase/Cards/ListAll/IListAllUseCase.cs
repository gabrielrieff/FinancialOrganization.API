using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Communication.Response.Cards;

namespace FinancialOrganization.API.Application.UseCase.Cards.ListAll;

public interface IListAllUseCase
{
    Task<AllCards> Execute(CancellationToken cancellationToken);
}
