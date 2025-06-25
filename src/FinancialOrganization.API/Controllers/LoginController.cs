using FinancialOrganization.API.Application.UseCase.Login;
using FinancialOrganization.API.Communication.Request.Login;
using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Communication.Response.Login;
using Microsoft.AspNetCore.Mvc;

namespace FinancialOrganization.API.Controllers;
[Route("v1/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(LoginResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase useCase,
        [FromBody] LoginRequestJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
