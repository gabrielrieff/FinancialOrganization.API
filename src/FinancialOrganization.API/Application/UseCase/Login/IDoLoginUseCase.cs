using FinancialOrganization.API.Communication.Request.Login;
using FinancialOrganization.API.Communication.Response.Login;

namespace FinancialOrganization.API.Application.UseCase.Login;

public interface IDoLoginUseCase
{
    Task<LoginResponseJson> Execute(LoginRequestJson request);
}
