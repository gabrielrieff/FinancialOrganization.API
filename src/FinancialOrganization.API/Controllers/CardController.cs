using FinancialOrganization.API.Application.UseCase.Cards.Delete;
using FinancialOrganization.API.Application.UseCase.Cards.GetAll;
using FinancialOrganization.API.Application.UseCase.Cards.ListAll;
using FinancialOrganization.API.Application.UseCase.Cards.Register;
using FinancialOrganization.API.Application.UseCase.Cards.Update;
using FinancialOrganization.API.Communication.Request;
using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Communication.Response.Cards;
using FinancialOrganization.API.Domain.Enums;
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

    [HttpGet()]
    [Route("search-list")]
    [ProducesResponseType(typeof(RegisterCardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchList(
        [FromServices] ISearchListUseCase useCase,
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
    
    [HttpGet()]
    [ProducesResponseType(typeof(RegisterCardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> List([FromServices] IListAllUseCase useCase, CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(cancellationToken);

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}/update")]
    [ProducesResponseType(typeof(RegisterCardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateCardRequest request, 
        [FromRoute] Guid id, 
        [FromServices] IUpdateCardUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(request, id, cancellationToken);

        return Ok();
    }
    
    [HttpDelete]
    [Route("{id}/dalete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id, 
        [FromServices] IDeleteCardUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.Execute(id, cancellationToken);

        return NoContent();
    }
}
