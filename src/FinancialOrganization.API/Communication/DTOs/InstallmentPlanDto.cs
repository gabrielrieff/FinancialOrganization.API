using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Communication.DTOs;

public class InstallmentPlanDto
{
    public Guid Id { get; set; }
    public int TotalInstallments { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public Guid MovementId { get; set; }
    public Guid? CardID { get; set; }

    public MovementDto Movement { get; set; } = default!;
    public Card? Card { get; set; } = default!;
    public List<InstallmentDto> Installments { get; set; } = new List<InstallmentDto>();
}
