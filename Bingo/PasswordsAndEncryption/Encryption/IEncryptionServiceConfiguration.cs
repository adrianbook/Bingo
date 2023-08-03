namespace PasswordsAndEncryption.Encryption;

public interface IEncryptionServiceConfiguration
{
    public string Password { get; }
    public byte[] Salt { get; }
}