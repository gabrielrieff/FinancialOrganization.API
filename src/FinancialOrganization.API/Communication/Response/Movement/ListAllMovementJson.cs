using FinancialOrganization.API.Domain.Enums;

namespace FinancialOrganization.API.Communication.Response.Movement;

public class ListAllMovementJson
{
    public MovementType Type { get; set; }
    public decimal AmountTotal { get; set; }
    public string Description { get; set; }
    public CategoryType Category { get; set; }
    public Status Status { get; set; }
    public InstallmentPlanJson InstallmentPlan { get; set; }
    public Guid? CardID { get; set; }
}

public class InstallmentPlanJson
{
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public ICollection<InstallmentJson> Installments { get; set; }
}

public class InstallmentJson
{
    public int InstallmentNumber { get; set; }
    public Status Status { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}
