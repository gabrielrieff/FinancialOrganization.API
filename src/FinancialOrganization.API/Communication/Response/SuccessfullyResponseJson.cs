namespace FinancialOrganization.API.Communication.Response;

public class SuccessfullyResponseJson
{
    public string Message { get; set; }

    public SuccessfullyResponseJson(string message)
    {
        Message = message;
    }

}
