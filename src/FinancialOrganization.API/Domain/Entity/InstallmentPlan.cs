using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Domain.Entity;

public class InstallmentPlan : EntityBase
{
    public int TotalInstallment { get;  set; }
    public DateTime InitialDate { get;  set; }
    public DateTime FinalDate { get;  set; }
    public Guid MovementId { get;  set; }
    public Guid? CardID { get;  set; }
    public Movement Movement { get;  set; } = default!;
    public Card? Card { get;  set; } = default!;
    public List<Installment> Installments { get;  set; } = new List<Installment>();

    public InstallmentPlan(
        int totalInstallment,
        DateTime initialDate,
        Guid movementId,
        Guid? cardID = null)
    {
        TotalInstallment = totalInstallment;
        InitialDate = initialDate;
        FinalDate = totalInstallment > 1 ? initialDate.AddMonths(totalInstallment) : initialDate;
        MovementId = movementId;
        CardID = cardID;

        Validate();
    }

    public void UpdateTotalInstallment(int totalInstallment)
    {
        TotalInstallment = totalInstallment;
        UpdatedAt = DateTime.UtcNow;
        FinalDate = InitialDate.AddMonths(TotalInstallment); // Ensure FinalDate is updated
        Validate();
    }

    public void UpdateDates(DateTime initialDate, DateTime finalDate)
    {
        InitialDate = initialDate;
        FinalDate = finalDate;
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    public void UpdateCard(Guid cardID)
    {
        CardID = cardID;
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    public void setInstalments(List<Installment> installments)
    {
        Installments = installments;
    }

    public void Validate()
    {
        var errors = new List<string>();

        if (TotalInstallment <= 0)
            errors.Add("Installment must be greater than 0.");
        if (InitialDate == default)
            errors.Add("Initial Date is required.");
        if (FinalDate == default)
            errors.Add("Final Date is required.");
        if (FinalDate < InitialDate)
            errors.Add("Final Date must be after InitialDate.");
        if (MovementId == Guid.Empty)
            errors.Add("MovementId is required.");
        if (CardID == Guid.Empty)
            errors.Add("CardID is required.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
