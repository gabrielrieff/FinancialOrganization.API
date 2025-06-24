using System.Text.RegularExpressions;
using FinancialOrganization.API.Exception.ExceptionsBase;

namespace FinancialOrganization.API.Domain.Entity;

public class User : EntityBase
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Guid UserIdendifier { get; private set; }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        UserIdendifier = Guid.NewGuid();
        ValidateEmail();
        //ValidatePassword();
    }

    public void setName(string name)
    {
        Name = name;
    }
    public void setEmail(string email)
    {
        Email = email;
        ValidateEmail();
    }
    public void setPassword(string password)
    {
        Password = password;
        //ValidatePassword();
    }


    #region Validators
    void ValidateEmail()
    {
        var regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Email))
            errors.Add("InstallmentNumber must be greater than 0.");
        if(!regex.IsMatch(Email))
            errors.Add("Email is not valid.");


        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
    
    void ValidatePassword()
    {
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*_\-+=])[A-Za-z\d!@#$%^&*_\-+=]{8,}$", RegexOptions.Compiled);
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Password))
            errors.Add("Password must not be empty.");
        if (!regex.IsMatch(Password))
            errors.Add("Password must be at least 8 characters long and contain uppercase, lowercase, digit, and special character.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
    #endregion



}

