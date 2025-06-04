using FinancialOrganization.API.Domain.Enums;

namespace FinancialOrganization.API.Communication.Request.Moviment;

public class UpdateMovementStatusJson
{
    public Status Status { get; set; }
}
