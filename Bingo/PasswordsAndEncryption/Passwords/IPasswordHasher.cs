namespace PasswordsAndEncryption.Passwords;
public interface IPasswordHasher
{
    public string GenerateHash(string password);
    public bool PasswordMatchesHash(string password, string hash);
}
