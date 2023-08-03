using Microsoft.Extensions.Configuration;

namespace PasswordsAndEncryption.Encryption;
public class EncryptionServiceConfigurationDI : EncryptionServiceConfiguration
{
    public EncryptionServiceConfigurationDI(IConfiguration configuration) : base(
        password: configuration["Encryption:Password"],
        salt: Convert.FromBase64String(configuration["Encryption:Salt"]))
    { }
}

