using PasswordsAndEncryption.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PasswordsAndEncryptionTest;
[TestClass]
public class Base64EncrypterTests
{
    private static string password = "password";
    private static byte[] salt = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};

    [TestMethod]
    public void TestEncryptingEmptyArrayReturnsNull()
    {
        var encryptedString = Base64Encrypter.EncryptByteArray(new byte[0], password, salt);
        Assert.IsNull(encryptedString);

    }
    [TestMethod]
    public void TestEncryptingWhiteSpace()
    {
        var encryptedString = Base64Encrypter.EncryptString(" ", password, salt);

        Assert.IsNull(encryptedString);
    }

    [TestMethod]
    public void TestCorruptedString()
    {
        var kosherString = Base64Encrypter.EncryptString("hejHopp",password, salt);

        Assert.IsTrue(kosherString!.Contains('='));

        var corruptedString = kosherString!.Substring(0, kosherString.Length - 1);

        Assert.IsTrue(corruptedString.Length == kosherString!.Length - 1);

        Assert.ThrowsException<FormatException>(() => Base64Encrypter.DecryptIntoString(corruptedString, password, salt));  
    }

    [TestMethod]
    public void TestEncryptingNullStringReturnsNull()
    {
        var encryptedString = Base64Encrypter.EncryptString(null, password, salt);
        Assert.IsNull(encryptedString);
    }
}
