using FinancialOrganization.API.Domain.Enums;

namespace FinancialOrganization.API.Communication.DTOs;

public class MovementDto
{
    public Guid Id { get; set; }
    public MovementType Type { get; set; }
    public decimal AmountTotal { get; set; }
    public string Description { get; set; } = string.Empty;
    public CategoryType Category { get; set; }
    public Status Status { get; set; }
    public InstallmentPlanDto InstallmentPlan { get; set; } = default!;
    public Guid? InstallmentPlanId { get; set; }
    public CardDto Card { get; set; } = default!;
    public Guid? CardID { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
