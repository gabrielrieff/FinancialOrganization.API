using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Application.UseCase.Cards.Register;

public class RegisterCardUseCase : IRegisterCardUseCase
{
    public RegisterCardUseCase()
    {
    }

    public RegisterCardResponse Execute(RegisterCardRequest request)
    {
        var card = new Card(request.Name);

        return new RegisterCardResponse
        {
            Id = card.Id,
            Name = card.Name
        };
    }
}
