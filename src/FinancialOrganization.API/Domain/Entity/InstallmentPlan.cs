using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Domain.Entity;

public class InstallmentPlan : EntityBase
{
    public int TotalInstallment { get; private set; }
    public DateTime InitialDate { get; private set; }
    public DateTime FinalDate { get; private set; }
    public Guid MovementId { get; private set; }
    public Guid CardID { get; private set; }

    // Navigation properties
    public Movement Movement { get; private set; } = default!;
    public Card Card { get; private set; } = default!;
    public ICollection<Installment> Installments { get; private set; } = new List<Installment>();

    public InstallmentPlan(
        int totalInstallment,
        DateTime initialDate,
        Guid movementId,
        Guid cardID)
    {
        TotalInstallment = totalInstallment;
        InitialDate = initialDate;
        FinalDate = initialDate.AddMonths(totalInstallment);
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

    public void Validate()
    {
        var errors = new List<string>();

        if (TotalInstallment <= 0)
            errors.Add("TotalInstallment must be greater than 0.");
        if (InitialDate == default)
            errors.Add("InitialDate is required.");
        if (FinalDate == default)
            errors.Add("FinalDate is required.");
        if (FinalDate < InitialDate)
            errors.Add("FinalDate must be after InitialDate.");
        if (MovementId == Guid.Empty)
            errors.Add("MovementId is required.");
        if (CardID == Guid.Empty)
            errors.Add("CardID is required.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
