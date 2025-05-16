using System.Security.Authentication;
using System.Security.Cryptography;
using ContactsApp.Backend.Data;
using Microsoft.AspNetCore.Identity;

namespace ContactsApp.Backend.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 512 / 8;
    private const int Iterations = 100000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
    
    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, KeySize);

        return string.Join('-', Convert.ToHexString(hash), Convert.ToHexString(salt));
    }

    public bool Verify(string passwordHash, string providedPassword)
    {
        string[] passwordParts = passwordHash.Split('-');
        byte[] hash = Convert.FromHexString(passwordParts[0]);
        byte[] salt = Convert.FromHexString(passwordParts[1]);

        byte[] providedPasswordHash = Rfc2898DeriveBytes.Pbkdf2(providedPassword, salt, Iterations, Algorithm, KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, providedPasswordHash);
    }
}