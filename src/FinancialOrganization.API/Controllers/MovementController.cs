using FinancialOrganization.API.Application.UseCase.Movements.Delete;
using FinancialOrganization.API.Application.UseCase.Movements.ListAll;
using FinancialOrganization.API.Application.UseCase.Movements.Register;
using FinancialOrganization.API.Application.UseCase.Movements.SearchList;
using FinancialOrganization.API.Application.UseCase.Movements.Update;
using FinancialOrganization.API.Application.UseCase.Movements.UpdateAmount;
using FinancialOrganization.API.Application.UseCase.Movements.UpdateStatus;
using FinancialOrganization.API.Communication.Request;
using FinancialOrganization.API.Communication.Request.Moviment;
using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Communication.Response.Movement;
using FinancialOrganization.API.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialOrganization.API.Controllers;
[Route("v1/[controller]")]
[Authorize]
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
    [Route("get-by-date-range")]
    public async Task<IActionResult> Get(
        [FromServices] IGetByDateRangeMovementsUseCase useCase,
        [FromQuery] DateTime initialDate,
        [FromQuery] DateTime endDate,
         CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(initialDate, endDate);
        return Ok(result);
    }

    [HttpGet()]
    [Route("search-list")]
    [ProducesResponseType(typeof(MovementJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchList(
    [FromServices] ISearchListMovementUseCase useCase,
    CancellationToken cancellationToken,
    [FromQuery] int? page = null,
    [FromQuery] DateTime? searchDate = null,
    [FromQuery(Name = "per_page")] int? perPage = null,
    [FromQuery] string? search = null,
    [FromQuery] string? sort = null,
    [FromQuery] SearchOrder? dir = null)
    {
        var request = new SearchListRequest();

        if (page is not null) request.Page = page.Value;
        if (searchDate is not null) request.SearchDate = searchDate.Value;
        if (perPage is not null) request.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) request.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) request.Sort = sort;
        if (dir is not null) request.Dir = dir.Value;

        var result = await useCase.Execute(request, cancellationToken);

        return Ok(result);

    }

    [HttpPut]
    [Route("{id}/update-amount")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SuccessfullyResponseJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAmount(
    [FromBody] UpdateMovementAmountJson request,
    [FromRoute] Guid id,
    [FromServices] IUpdateAmountMovementUseCase useCase,
    CancellationToken cancellationToken)
    {
        await useCase.Execute(id, request, cancellationToken);

        var message = new SuccessfullyResponseJson("Updated the value successfully.");

        return Ok(message);
    }

    [HttpPut]
    [Route("{id}/update-status")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SuccessfullyResponseJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStatus(
    [FromBody] UpdateMovementStatusJson request,
    [FromRoute] Guid id,
    [FromServices] IUpdateStatusMovementUseCase useCase,
    CancellationToken cancellationToken)
    {
        await useCase.Execute(id, request, cancellationToken);

        var message = new SuccessfullyResponseJson("Updated the status successfully.");

        return Ok(message);
    }
    
    [HttpPut]
    [Route("{id}/update")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SuccessfullyResponseJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(
    [FromBody] UpdateMovementJson request,
    [FromRoute] Guid id,
    [FromServices] IUpdateMovementUseCase useCase,
    CancellationToken cancellationToken)
    {
        await useCase.Execute(id, request, cancellationToken);

        var message = new SuccessfullyResponseJson("Updated the successfully.");

        return Ok(message);
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SuccessfullyResponseJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(
    [FromRoute] Guid id,
    [FromServices] IDeleteMovementUseCase useCase,
    CancellationToken cancellationToken)
    {
        await useCase.Execute(id, cancellationToken);

        var message = new SuccessfullyResponseJson("Delete the successfully.");

        return Ok(message);
    }
}
