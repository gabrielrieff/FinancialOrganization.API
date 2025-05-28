using FinancialOrganization.API.Domain.Enums;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Domain.Entity;

public class Installment : EntityBase
{
    public int InstallmentNumber { get; private set; }
    public Status Status { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DueDate { get; private set; }
    public Guid InstallmentPlanId { get; private set; }

    public InstallmentPlan InstallmentPlan { get; private set; } = default!;

    public Installment(
        int installmentNumber,
        Status status,
        decimal amount,
        DateTime dueDate,
        Guid installmentPlanId)
    {
        InstallmentNumber = installmentNumber;
        Status = status;
        Amount = amount;
        DueDate = dueDate;
        InstallmentPlanId = installmentPlanId;

        Validate();
    }

    public void UpdateStatus(Status status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    public void UpdateAmount(decimal amount)
    {
        Amount = amount;
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    public void UpdateDueDate(DateTime dueDate)
    {
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    public void Validate()
    {
        var errors = new List<string>();

        if (InstallmentNumber <= 0)
            errors.Add("InstallmentNumber must be greater than 0.");
        if (Amount <= 0)
            errors.Add("Amount must be greater than 0.");
        if (DueDate == default)
            errors.Add("DueDate is required.");
        if (InstallmentPlanId == Guid.Empty)
            errors.Add("InstallmentPlanId is required.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
