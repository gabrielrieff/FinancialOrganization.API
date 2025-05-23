using FinancialOrganization.API.Application.UseCase.Cards.Register;
using FinancialOrganization.API.Application.UseCase.Cards.Update;
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
    public async Task<IActionResult> Register(
        [FromBody] RegisterCardRequest request,
        [FromServices] IRegisterCardUseCase useCase,
        CancellationToken cancellationToken)
    {
            var result = await useCase.Execute(request, cancellationToken);

           return Created(string.Empty, result);

    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(RegisterCardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateCardRequest request, 
        [FromRoute] Guid id, 
        [FromServices] IUpdateCardUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.Execute(request, id, cancellationToken);

        return Ok();
    }
}
