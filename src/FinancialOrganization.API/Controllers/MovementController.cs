using FinancialOrganization.API.Application.UseCase.Movements.ListAll;
using FinancialOrganization.API.Application.UseCase.Movements.Register;
using FinancialOrganization.API.Communication.Request.Moviment;
using Microsoft.AspNetCore.Mvc;

namespace FinancialOrganization.API.Controllers;
[Route("v1/[controller]")]
[ApiController]
public class MovementController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterMovementUseCase useCase,
        [FromBody] MovimentRegisterJson request,
         CancellationToken cancellationToken)
    {
        await useCase.Execute(request, cancellationToken);
        return Ok();
    }


    [HttpGet]
    public async Task<IActionResult> Get(
        [FromServices] IListAllMovementsUseCase useCase,
         CancellationToken cancellationToken)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }
}
