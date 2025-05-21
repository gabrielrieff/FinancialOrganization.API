namespace FinancialOrganization.API.Exception.ExceptionsBase;

public abstract class FinancialOrganizationException : SystemException
{
    protected FinancialOrganizationException(string message) : base(message)
    { }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
