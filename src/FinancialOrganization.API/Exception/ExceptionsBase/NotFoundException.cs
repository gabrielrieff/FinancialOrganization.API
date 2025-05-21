using System.Net;

namespace FinancialOrganization.API.Exception.ExceptionsBase;
public class NotFoundException : FinancialOrganizationException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}