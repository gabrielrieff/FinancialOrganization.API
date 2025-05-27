using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinancialOrganization.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is FinancialOrganizationException)
        {
            HandleProjecException(context);
        }
        else
        {
            ThrowUnkowError(context);

        }
    }

    private void HandleProjecException(ExceptionContext context)
    {
        var financialORganizationException = (FinancialOrganizationException)context.Exception;
        var errorMessage = new ResponseErrorJson(financialORganizationException.GetErrors());

        context.HttpContext.Response.StatusCode = financialORganizationException.StatusCode;
        context.Result = new ObjectResult(errorMessage);
    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson("An unexpected error occurred. Please try again later.");
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
