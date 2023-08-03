using DataTransferUtility;
using System.Text.Json;

namespace PasswordsAndEncryption.Encryption;
public class EncryptionService : IEncryptionService
{
    private readonly byte[] _salt;
    private readonly string _password;

    public EncryptionService(IEncryptionServiceConfiguration config)
    {
        _salt=config.Salt;
        _password=config.Password;
    }

    public Result<byte[]> DecryptIntoByteArray(string trunkatedBase64)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(trunkatedBase64))
            {
                return new ArgumentNullException(nameof(trunkatedBase64));
            }
            string base64 = ParseTrunkatedBase64String(trunkatedBase64);
            byte[]? decryptedBytes = Base64Encrypter.DecryptIntoByteArray(base64, _password, _salt);
            return decryptedBytes!;
        }
        catch (Exception ex)
        {
            return new EncryptionException("Decryption failed", ex);
        }
    }

    public Result<string> DecryptIntoString(string trunkatedBase64)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(trunkatedBase64))
            {
                return new ArgumentNullException(nameof(trunkatedBase64));
            }
            string base64 = ParseTrunkatedBase64String(trunkatedBase64);
            string decryptedString = Base64Encrypter.DecryptIntoString(base64, _password, _salt);
            return decryptedString!;
        }
        catch (Exception ex)
        {
            return new EncryptionException("Decryption failed", ex);
        }
    }


    public Result<T> DecryptInto<T>(string trunkatedBase64) where T : class
    {
        try
        {
            if (string.IsNullOrWhiteSpace(trunkatedBase64))
            {
                return new ArgumentNullException(nameof(trunkatedBase64));
            }
            string base64 = ParseTrunkatedBase64String(trunkatedBase64);
            string decryptedString = Base64Encrypter.DecryptIntoString(base64, _password, _salt);

            return JsonSerializer.Deserialize<T>(decryptedString!) ?? throw new ArgumentNullException(nameof(T));
        }
        catch (Exception ex)
        {
            return new EncryptionException("Decryption failed", ex);
        }
    }

    public Result<string> Encrypt(byte[] data)
    {
        try
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            string? base64 = Base64Encrypter.EncryptByteArray(data, _password, _salt);
            return TrunkateBase64String(base64!);
        }
        catch (Exception ex)
        {
            return new EncryptionException("Encryption failed", ex);
        }
    }

    public Result<string> Encrypt(string data)
    {
        try
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            string? base64 = Base64Encrypter.EncryptString(data, _password, _salt);
            return TrunkateBase64String(base64!);
        }
        catch (Exception ex)
        {
            return new EncryptionException("Encryption failed", ex);
        }
    }
    public Result<string> Encrypt<T>(T data) where T : class
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }
        return Encrypt(JsonSerializer.Serialize(data));
    }

    internal string TrunkateBase64String(string base64)
    {
        return base64.Replace('+', '-').Replace('/', '_').Replace("=", "");
    }

    internal string ParseTrunkatedBase64String(string trunkatedBase64)
    {
        return trunkatedBase64.Replace('-', '+').Replace('_', '/').PadRight(trunkatedBase64.Length+(4 - (trunkatedBase64.Length % 4)), '=');
    }

}
