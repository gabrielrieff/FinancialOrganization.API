namespace FinancialOrganization.API.Exception.ExceptionsBase;

public class ErrorOnValidationException : FinancialOrganizationException
{
    public List<string> Errors { get; set; } = [];

    public ErrorOnValidationException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}
