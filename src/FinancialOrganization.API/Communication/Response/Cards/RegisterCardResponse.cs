namespace FinancialOrganization.API.Communication.Response.Cards;

public class RegisterCardResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public RegisterCardResponse(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
