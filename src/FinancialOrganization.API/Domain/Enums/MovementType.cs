using System.ComponentModel;

namespace FinancialOrganization.API.Domain.Enums;

public enum MovementType
{
    [Description("Revenue")]
    Revenue,
    [Description("Expense")]
    Expense

}
