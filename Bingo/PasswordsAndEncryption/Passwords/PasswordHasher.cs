using System.Security.Cryptography;

namespace PasswordsAndEncryption.Passwords;
public class PasswordHasher : IPasswordHasher
{
    private readonly Random _random;
    private readonly int _saltSize = 16;
    private readonly int _passwordHashSize = 20;
    private readonly int _iterations = 100000;

    public PasswordHasher()
    {
        _random = new Random();
    }

    public string GenerateHash(string password)
    {
        var salt = new byte[_saltSize];
        _random.NextBytes(salt);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations);
        byte[] hash = pbkdf2.GetBytes(_passwordHashSize);

        byte[] hashBytes = new byte[_passwordHashSize + _saltSize];
        Array.Copy(salt, 0, hashBytes, 0, _saltSize);
        Array.Copy(hash, 0, hashBytes, _saltSize, _passwordHashSize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool PasswordMatchesHash(string password, string hash)
    {
        var hashBytes = Convert.FromBase64String(hash);

        if (hashBytes.Length != (_passwordHashSize + _saltSize))
        {
            throw new ArgumentException("Invalid hash-lenght for this hasher");
        }

        var salt = new byte[_saltSize];
        Array.Copy(hashBytes, 0, salt, 0, _saltSize);

        var storedHash = new byte[_passwordHashSize];
        Array.Copy(hashBytes, _saltSize, storedHash, 0, _passwordHashSize);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations);
        var incomingHash = pbkdf2.GetBytes(_passwordHashSize);

        for (int i = 0; i < _passwordHashSize; i++)
        {
            if (storedHash[i] != incomingHash[i])
            {
                return false;
            }
        }
        return true;
    }
}
