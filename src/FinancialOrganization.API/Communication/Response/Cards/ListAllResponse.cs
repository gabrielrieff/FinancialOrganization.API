namespace FinancialOrganization.API.Communication.Response.Cards;

public class ListAllResponse
{
    public IReadOnlyList<RegisterCardResponse> Items { get; set; } = [];
}
