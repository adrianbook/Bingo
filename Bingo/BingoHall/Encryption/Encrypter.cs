using System.IO.Compression;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Runtime.CompilerServices;


namespace BingoHall.Encryption
{
    internal static class Encrypter
    {
        internal static string? EncryptString(string? dataString, string password, byte[] salt)
        {
            if (dataString is null)
            {
                return null;
            }

            return EncryptByteArray(Encoding.UTF8.GetBytes(dataString), password, salt);
            
        }

        internal static string? EncryptByteArray(byte[]? data, string password, byte[] salt)
        {
            if (data is null)
            {
                return null;
            }
            var encryptedData = Encrypt(data, password, salt);

            var base64String = Convert.ToBase64String(encryptedData);

            return base64String;
        }

        internal static string DecryptString(string dataString, string password, byte[] salt)
        {
            if (string.IsNullOrEmpty(dataString))
            {
                return null!;
            }
            var encryptedData = Convert.FromBase64String(dataString);
            var decryptedData = Decrypt(encryptedData, password, salt);
            var decryptedString = Encoding.UTF8.GetString(decryptedData);
            return decryptedString;
        }

        private static byte[] Encrypt(byte[] data, string password, byte[] salt)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Failed to encrypt data", ex);
            }
        }
        private static byte[] Decrypt(byte[] data, string password, byte[] salt)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Failed to decrypt data", ex);
            }
        }


        private static byte[] Unzip(byte[] input)
        {
            try
            {
                using var source = new MemoryStream(input);

                // read length of data
                var lengthBytes = new byte[4];
                source.Read(lengthBytes, 0, 4);
                var length = BitConverter.ToInt32(lengthBytes, 0);

                source.Position = 4;
                using var decompressionStream = new GZipStream(source, CompressionMode.Decompress);
                var result = new byte[length];
                var bytesReadTotal = 0;
                while (bytesReadTotal < length)
                {
                    var bytesRead = decompressionStream.Read(result, bytesReadTotal, length - bytesReadTotal);
                    if (bytesRead == 0)
                    {
                        throw new InvalidDataException("Unexpected end of data");
                    }

                    bytesReadTotal += bytesRead;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to unzip data", ex);
            }
        }

        private static byte[] Zip(byte[] input)
        {
            try
            {
                using var result = new MemoryStream();

                // record total length of input data at beginning
                var lengthBytes = BitConverter.GetBytes(input.Length);
                result.Write(lengthBytes, 0, 4);

                using var compressionStream = new GZipStream(result, CompressionMode.Compress);
                compressionStream.Write(input, 0, input.Length);
                compressionStream.Close();

                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to zip data", ex);
            }
        }
    }
}
