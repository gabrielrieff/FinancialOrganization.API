using FinancialOrganization.API.Domain.Enums;

namespace FinancialOrganization.API.Communication.Request.Moviment;

public class UpdateMovementJson
{
    public Guid? CardId { get; set; }
    public CategoryType? CategoryType { get; set; }
    public string? Description { get; set; }
}
