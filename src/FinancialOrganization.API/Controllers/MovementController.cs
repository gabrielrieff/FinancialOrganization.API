using FinancialOrganization.API.Application.UseCase.Movements.ListAll;
using FinancialOrganization.API.Application.UseCase.Movements.Register;
using FinancialOrganization.API.Application.UseCase.Movements.SearchList;
using FinancialOrganization.API.Communication.Request.Moviment;
using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Enums;
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

    [HttpGet()]
    [Route("search-list")]
    [ProducesResponseType(typeof(RegisterCardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchList(
    [FromServices] ISearchListMovementUseCase useCase,
    CancellationToken cancellationToken,
    [FromQuery] int? page = null,
    [FromQuery(Name = "per_page")] int? perPage = null,
    [FromQuery] string? search = null,
    [FromQuery] string? sort = null,
    [FromQuery] SearchOrder? dir = null)
    {
        var request = new SearchListRequest();
        if (page is not null) request.Page = page.Value;
        if (perPage is not null) request.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) request.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) request.Sort = sort;
        if (dir is not null) request.Dir = dir.Value;

        var result = await useCase.Execute(request, cancellationToken);

        return Ok(result);

    }
}
