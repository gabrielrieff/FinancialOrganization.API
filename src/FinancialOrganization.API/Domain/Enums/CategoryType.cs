using System.ComponentModel;

namespace FinancialOrganization.API.Domain.Enums;

public enum CategoryType
{
    [Description("Food")]
    Food,
    [Description("Transportation")]
    Transportation,
    [Description("Housing")]
    Housing,
    [Description("Education")]
    Education,
    [Description("Leisure")]
    Leisure,
    [Description("Health")]
    Health,
    [Description("Subscriptions and Services")]
    Subscriptions,
    [Description("Shopping")]
    Shopping,
    [Description("Bills and Utilities")]
    Utilities,
    [Description("Taxes and Fees")]
    Taxes,
    [Description("Investments")]
    Investments,
    [Description("Donations")]
    Donations,
    [Description("Travel")]
    Travel,
    [Description("Other")]
    Other
}
