using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;


[assembly: InternalsVisibleTo("PasswordsAndEncryptionTest")]
namespace PasswordsAndEncryption.Encryption;

internal static class Base64Encrypter
{
    public static string? EncryptString(string? dataString, string password, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(dataString))
        {
            return null;
        }

        return EncryptByteArray(Encoding.UTF8.GetBytes(dataString), password, salt);
    }

    public static string? EncryptByteArray(byte[]? data, string password, byte[] salt)
    {
        if (data is null || !data.Any())
        {
            return null;
        }
        byte[] encryptedData = Encrypt(data, password, salt);

        string base64String = Convert.ToBase64String(encryptedData);

        return base64String;
    }

    public static string DecryptIntoString(string base64String, string password, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(base64String))
        {
            return null!;
        }
        byte[]? bytes = DecryptIntoByteArray(base64String, password, salt);
        return Encoding.UTF8.GetString(bytes!);
    }

    public static byte[]? DecryptIntoByteArray(string base64String, string password, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(base64String))
        {
            return null!;
        }
        byte[] encryptedData = Convert.FromBase64String(base64String);
        byte[] decryptedData = Decrypt(encryptedData, password, salt);
        return decryptedData;
    }

    private static byte[] Encrypt(byte[] data, string password, byte[] salt)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, salt);
        using var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.KeySize = 256;
        aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
        aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);

        using var plainStream = new MemoryStream(data);
        using var encryptedStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(encryptedStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            plainStream.CopyTo(cryptoStream);
        }
        return encryptedStream.ToArray();
    }
    private static byte[] Decrypt(byte[] data, string password, byte[] salt)
    {
        // setup decrypter
        using var deriveBytes = new Rfc2898DeriveBytes(password, salt);
        using var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.KeySize = 256;
        aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
        aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);

        // decrypt
        using var cipherStream = new MemoryStream(data);
        using var decryptedStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(cipherStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        cryptoStream.CopyTo(decryptedStream);

        return decryptedStream.ToArray();
    }


    private static byte[] Unzip(byte[] input)
    {
        using var source = new MemoryStream(input);

        // read length of data
        byte[] lengthBytes = new byte[4];
        source.Read(lengthBytes, 0, 4);
        int length = BitConverter.ToInt32(lengthBytes, 0);

        source.Position = 4;
        using var decompressionStream = new GZipStream(source, CompressionMode.Decompress);
        byte[] result = new byte[length];
        int bytesReadTotal = 0;
        while (bytesReadTotal < length)
        {
            int bytesRead = decompressionStream.Read(result, bytesReadTotal, length - bytesReadTotal);
            if (bytesRead == 0)
            {
                throw new InvalidDataException("Unexpected end of data");
            }

            bytesReadTotal += bytesRead;
        }
        return result;
    }

    private static byte[] Zip(byte[] input)
    {
        using var result = new MemoryStream();

        // record total length of input data at beginning
        byte[] lengthBytes = BitConverter.GetBytes(input.Length);
        result.Write(lengthBytes, 0, 4);

        using var compressionStream = new GZipStream(result, CompressionMode.Compress);
        compressionStream.Write(input, 0, input.Length);
        compressionStream.Close();

        return result.ToArray();
    }
}

