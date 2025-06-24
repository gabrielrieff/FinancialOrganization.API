using FinancialOrganization.API.Application.UseCase.Installments.UpdateStatus;
using FinancialOrganization.API.Communication.Request.Installment;
using FinancialOrganization.API.Communication.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialOrganization.API.Controllers;
[Route("v1/[controller]")]
[Authorize]
[ApiController]
public class InstallmentController : ControllerBase
{
    [HttpPut]
    [Route("{id}/update-status")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SuccessfullyResponseJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStatus(
        [FromBody] UpdateStatusInstallmentJson request,
        [FromRoute] Guid id,
        [FromServices] IUpdateStatusInstallmentUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(id, request, cancellationToken);

        var message = new SuccessfullyResponseJson("Updated the status successfully.");

        return Ok(message);
    }
}
