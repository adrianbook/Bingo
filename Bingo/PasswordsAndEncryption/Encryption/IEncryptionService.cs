using DataTransferUtility;

namespace PasswordsAndEncryption.Encryption;
public interface IEncryptionService
{
    public Result<string> Encrypt(byte[] data);
    public Result<string> Encrypt(string data);
    public Result<string> Encrypt<T>(T data) where T : class;
    public Result<byte[]> DecryptIntoByteArray(string trunkatedBase64);
    public Result<string> DecryptIntoString(string trunkatedBase64);
    public Result<T> DecryptInto<T>(string trunkatedBase64) where T : class;
}
