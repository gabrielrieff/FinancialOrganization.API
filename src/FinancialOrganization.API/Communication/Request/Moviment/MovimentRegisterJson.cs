using FinancialOrganization.API.Domain.Enums;

namespace FinancialOrganization.API.Communication.Request.Moviment;

public class MovimentRegisterJson
{
    public decimal AmountTotal { get; set; }
    public string Description { get; set; }
    public Guid? CardID { get; set; }
    public int Installments { get; set; }
    public DateTime InitialDate { get; set; }
    public MovementType Type { get; set; }
    public CategoryType Category { get; set; }
    public Status? Status { get; set; }
}
