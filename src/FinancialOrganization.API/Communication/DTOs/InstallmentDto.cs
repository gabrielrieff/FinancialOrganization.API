using FinancialOrganization.API.Domain.Enums;

namespace FinancialOrganization.API.Communication.DTOs;

public class InstallmentDto
{
    public Guid Id { get; set; }
    public int InstallmentNumber { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public Status Status { get; set; }
}
