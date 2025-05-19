namespace FinancialOrganization.API.Domain.Entity;

public class Card : EntityBase
{
    public string Name { get; private set; }

    public Card(string name)
    {
        Name = name;

        Validate();
    }

    #region Setters
     public void setName(string name)
    {
        Name = name;
        Validate();
    }
    #endregion


    #region Valitadors
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new Exception("Name is required");
        }
    }
    #endregion
}
