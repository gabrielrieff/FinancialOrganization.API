
using System.Net;

namespace FinancialOrganization.API.Exception.ExceptionsBase;

public class InvalidLoginException : FinancialOrganizationException
{
    public InvalidLoginException() : base("Email or Password Invalid")
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
