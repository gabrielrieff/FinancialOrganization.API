using FinancialOrganization.API.Domain.Enums;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Domain.Entity;

public class Movement : EntityBase
{
    public MovementType Type { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    public CategoryType Category { get; private set; }
    public Status Status { get; private set; }
    public bool IsReccuring { get; private set; }
    public long CardID { get; private set; }
    public Card Card { get; private set; } = default!;

    public Movement(
        MovementType type,
        decimal amount,
        string description,
        CategoryType category,
        long cardID,
        bool isReccuring = false,
        Status status = Status.Waiting)
    {
        Type = type;
        Amount = amount;
        Description = description;
        Category = category;
        Status = status;
        IsReccuring = isReccuring;
        CardID = cardID;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }


    #region Setters
    public void UpdateAmount(decimal amount)
    {
        Amount = amount;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }
    
    public void UpdateCard(long cardID)
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
            else if (Amount <= 0)
                error.Add("Amount must be greater than 0");
            else if (CardID <= 0)
                error.Add("CardID is required");
            else if (Category.Equals(typeof(CategoryType)))
                error.Add("Category is required");

            if(error.Count > 0)
            {
                throw new ErrorOnValidationException(error);
            }
        }
    #endregion
}
