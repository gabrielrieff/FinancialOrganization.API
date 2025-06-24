using FinancialOrganization.API.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace FinancialOrganization.API.Infrastructure.Security.Criptography;

internal class Encripty : IPasswordEncripter
{
    public string Encrypt(string password)
    {
       string passwordHash = BC.HashPassword(password);
        return passwordHash;
    }
}
