﻿namespace FinancialOrganization.API.Domain.Security.Cryptography;

public interface IPasswordEncripter
{
    string Encrypt(string password);
    bool Verify(string password, string encryptedPassword);
}
