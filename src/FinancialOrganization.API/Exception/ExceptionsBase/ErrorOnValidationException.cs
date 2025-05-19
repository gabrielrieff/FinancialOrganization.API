using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinancialOrganization.API.Exception.ExceptionsBase;

public class ErrorOnValidationException : FinancialOrganizationException
{
    public List<string> _errors { get; set; } = [];

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }

    public override List<string> GetErrors()
    {
        return _errors;
    }
}
