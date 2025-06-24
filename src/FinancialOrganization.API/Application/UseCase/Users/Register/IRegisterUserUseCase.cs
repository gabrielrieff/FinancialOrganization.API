using FinancialOrganization.API.Communication.Request.Users;
using FinancialOrganization.API.Communication.Response.Users;

namespace FinancialOrganization.API.Application.UseCase.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseUserJson> Execute(RegisterUserJson request, CancellationToken cancellationToken);
}
