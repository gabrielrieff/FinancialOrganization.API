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
    public InstallmentPlan? InstallmentPlan { get; private set; } = default!;
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
        Guid? installmentPlanId = null,
        Status status = Status.Waiting)
    {
        Type = type;
        AmountTotal = amountTotal;
        Description = description;
        Category = category;
        Status = status;
        //UserId = userId;
        CardID = cardID;
        InstallmentPlanId = installmentPlanId;

        Validate();
    }


    #region Setters
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
    #endregion
}
