using FinancialOrganization.API.Domain.Enums;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Domain.Entity;

public class Movement : EntityBase
{
    public MovementType Type { get; private set; }
    public decimal AmountTotal { get; private set; }
    public string Description { get; private set; }
    public CategoryType Category { get; private set; }
    public Status Status { get; private set; }
    public InstallmentPlan InstallmentPlan { get; private set; } = default!;
    public Guid? InstallmentPlanId { get; private set; }
    public Guid? CardID { get; private set; }
    public Card? Card { get; private set; } = default!;

    public Movement(
        MovementType type,
        decimal amountTotal,
        string description,
        CategoryType category,
        //Guid userId,
        Guid? cardID = null,
        Status status = Status.Waiting)
    {
        Type = type;
        AmountTotal = amountTotal;
        Description = description;
        Category = category;
        Status = status;
        //UserId = userId;
        CardID = cardID;

        Validate();
    }


    #region Setters
    public void SetInstallmentPlanId(Guid installmentPlanId)
    {
        InstallmentPlanId = installmentPlanId;
    }

    public void SetStatus(Status status)
    {
        Status = status;
        ValidateSetStatus();
    }

    public void UpdateAmount(decimal amountTotal)
    {
        AmountTotal = amountTotal;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }

    public void UpdateCard(Guid cardID)
    {
        CardID = cardID;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }
    public void UpdateCategory(CategoryType category)
    {
        Category = category;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }
    
    public void UpdateDescription(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }
    #endregion


    #region Valitadors
        public void Validate()
        {
            List<string> error = [];

            if (string.IsNullOrWhiteSpace(Description))
                error.Add("Description is required");
            else if (Description.Length > 100)
                error.Add("Description must be less than 100 characters");
            else if (Description.Length < 10)
                error.Add("Description must be more than 10 characters");
            else if (AmountTotal <= 0)
                error.Add("Amount must be greater than 0");
            else if (Category.Equals(typeof(CategoryType)))
                error.Add("Category is required");

            if(error.Count > 0)
            {
                throw new ErrorOnValidationException(error);
            }
        }

    public void ValidateSetStatus()
    {
        List<string> error = [];

        foreach (var item in InstallmentPlan.Installments)
        {
            if(Status == Status.Paid && item.Status == Status.Waiting)
            {
                error.Add($"Cannot set status to Paid when there are installment {item.Id} with Waiting status.");
            }
        }

        if (error.Count > 0)
        {
            throw new ErrorOnValidationException(error);
        }
    }
    #endregion
}
