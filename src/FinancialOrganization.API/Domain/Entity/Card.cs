using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Domain.Entity;

public class Card : EntityBase
{
    public string Name { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;

    public Card(string name, Guid userId)
    {
        Name = name;
        UserId = userId;

        Validate();
    }

    #region Setters
     public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }
    #endregion


    #region Valitadors
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            var error = new ResponseErrorJson("Name is required");
            throw new ErrorOnValidationException(error.ErrorMessages);
        }
    }
    #endregion
}
