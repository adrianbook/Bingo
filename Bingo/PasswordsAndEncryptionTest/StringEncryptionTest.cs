using PasswordsAndEncryption.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordsAndEncryptionTest;
[TestClass]
public class StringEncryptionTest
{
    [TestMethod]
    public void TestEncryptionStringLength()
    {
        var rawString = @"""
        {
            ""username"": ""adrian@exsolve.se"",
            ""password"": ""password"",
        ""roles"": [
                ""Admin"",
                ""User""
            ]
        }
        """;

        var encryptedString = Base64Encrypter.EncryptString(rawString, "password", new byte[] {123, 23, 3, 4, 54, 12, 65, 12});

        Console.WriteLine(encryptedString.Length);

        Assert.IsTrue(encryptedString.Length < 1000);
    }
}
