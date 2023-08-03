namespace PasswordsAndEncryption.Encryption;
public class EncryptionServiceConfiguration : IEncryptionServiceConfiguration
{
    public EncryptionServiceConfiguration(string password, byte[] salt)
    {
        Password=password;
        Salt=salt;
    }

    public string Password { get; set; }
    public byte[] Salt { get; set; }


}
