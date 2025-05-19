using FinancialOrganization.API.Application.UseCase.Cards.Register;
using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Communication.Response.Cards;
using Microsoft.AspNetCore.Mvc;

namespace FinancialOrganization.API.Controllers;
[Route("v1/[controller]")]
[ApiController]
public class CardController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterCardResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RegisterCardRequest request, [FromServices] IRegisterCardUseCase useCase)
    {
        var result = useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
