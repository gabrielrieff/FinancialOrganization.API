using FinancialOrganization.API.Communication.Request.Users;
using FinancialOrganization.API.Communication.Response.Users;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Users;
using FinancialOrganization.API.Domain.Security.Cryptography;
using FinancialOrganization.API.Domain.Security.Tokens;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Application.UseCase.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passowrdEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;


    public RegisterUserUseCase(IUserRepository repo, IUnitOfWork unitOfWork, IPasswordEncripter passowrdEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
        _passowrdEncripter = passowrdEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseUserJson> Execute(RegisterUserJson request, CancellationToken cancellationToken)
    {
        var emailExist = await _repo.ExistActiveUserWithEmail(request.Email);

        var errors = new List<string>();

        if (emailExist)
            errors.Add("Email Already registered");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);

        var passwordEncripted = _passowrdEncripter.Encrypt(request.Password);
        var user = new User(request.Name, request.Email, passwordEncripted);

        await _repo.Register(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        var token = _accessTokenGenerator.Generate(user); 

        return new ResponseUserJson
        {
            Token = token
        };
    }
}
