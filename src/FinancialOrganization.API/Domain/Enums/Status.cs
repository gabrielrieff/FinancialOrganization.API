using System.ComponentModel;

namespace FinancialOrganization.API.Domain.Enums;

public enum Status
{
    [Description("Waiting")]
    Waiting,
    [Description("Paid")]
    Paid

}
