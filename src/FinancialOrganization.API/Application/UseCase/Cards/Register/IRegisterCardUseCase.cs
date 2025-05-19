using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Communication.Response.Cards;

namespace FinancialOrganization.API.Application.UseCase.Cards.Register;

public interface IRegisterCardUseCase
{
    RegisterCardResponse Execute(RegisterCardRequest request);
}
