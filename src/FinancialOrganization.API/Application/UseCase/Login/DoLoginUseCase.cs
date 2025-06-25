using FinancialOrganization.API.Communication.Request.Login;
using FinancialOrganization.API.Communication.Response.Login;
using FinancialOrganization.API.Domain.Repositories.Users;
using FinancialOrganization.API.Domain.Security.Cryptography;
using FinancialOrganization.API.Domain.Security.Tokens;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Login;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserRepository userRepo, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepo = userRepo;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<LoginResponseJson> Execute(LoginRequestJson request)
    {
        var user = await _userRepo.GetByEmail(request.Email);

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (!passwordMatch)
        {
            throw new InvalidLoginException();
        }

        return new LoginResponseJson
        {
            Token = _accessTokenGenerator.Generate(user),
        };
    }
}
