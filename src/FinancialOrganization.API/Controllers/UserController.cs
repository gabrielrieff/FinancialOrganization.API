using FinancialOrganization.API.Application.UseCase.Users.Register;
using FinancialOrganization.API.Communication.Request.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinancialOrganization.API.Controllers;
[Route("v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserJson request,
        [FromServices] IRegisterUserUseCase useCase,
        CancellationToken cancellationToken
        )
    {
        var result = await useCase.Execute(request, cancellationToken);
        return Created(string.Empty, result);
    }
}
