using System.Net;

namespace FinancialOrganization.API.Exception.ExceptionsBase;

public class ErrorOnValidationException : FinancialOrganizationException
{
    public List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }

    public override List<string> GetErrors()
    {
        return _errors;
    }
}
