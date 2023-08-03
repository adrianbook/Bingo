using PasswordsAndEncryption.Encryption;
using System.Text.Json;

namespace PasswordsAndEncryptionTest;
[TestClass]
public class EncryptionServiceTests
{
    private record TestRecord(string hej, int hopp);
    private IEncryptionService _encryptionService;
    [TestInitialize]
    public void Initialize()
    {
        _encryptionService = new EncryptionService(
            new EncryptionServiceConfiguration(
                password: "password",
                salt: new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                )
            );                
    }

    [TestMethod]
    public void TestDecryptingEmptyStringReturnsException()
    {
        var decryptedResult = _encryptionService.DecryptIntoString("");
        Assert.IsInstanceOfType( decryptedResult.Failure, typeof(ArgumentNullException) );
    }

    [TestMethod]
    public void TestDecryptingNullStringReturnsException()
    {
        var decryptedResult = _encryptionService.DecryptIntoString(null);
        Assert.IsInstanceOfType( decryptedResult.Failure, typeof(ArgumentNullException) );
    }
    [TestMethod]
    public void TestStringEncryptionDecryption()
    {
        var encryptedStringResult = _encryptionService.Encrypt("hejHopp");

        Assert.IsTrue(encryptedStringResult.IsSuccess);

        var decryptedString = _encryptionService.DecryptIntoString(encryptedStringResult.Value!);
        Assert.AreEqual("hejHopp", decryptedString.Value);
    }

    [TestMethod]
    public void TestByteArrayEncryptionDecryption()
    {
        var encryptedStringResult = _encryptionService.Encrypt(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
        Assert.IsTrue(encryptedStringResult.IsSuccess);
        var decryptedString = _encryptionService.DecryptIntoByteArray(encryptedStringResult.Value!);
        Assert.IsTrue(decryptedString.Value!.SequenceEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }));
    }

    [TestMethod]
    public void TestCorruptedString()
    {
        var kosherString = _encryptionService.Encrypt(new TestRecord("hej", 44));
        Assert.IsTrue(kosherString.IsSuccess);
        var corruptedString = kosherString.Value!.Substring(0, kosherString.Value.Length - 1);
        Assert.IsTrue(corruptedString.Length == kosherString.Value.Length - 1);
        var decryptedString = _encryptionService.DecryptInto<TestRecord>(corruptedString);
        Assert.IsInstanceOfType(decryptedString.Failure, typeof(EncryptionException));
    }

    [TestMethod]
    public void TestGenericEncryptionDecryption()
    {
        var encryptedResult = _encryptionService.Encrypt(new TestRecord("hej", 3));
        Assert.IsTrue(encryptedResult.IsSuccess);

        var decryptedResult = _encryptionService.DecryptInto<TestRecord>(encryptedResult.Value!);
        Assert.IsTrue(decryptedResult.IsSuccess);

        Console.WriteLine(decryptedResult.Value.ToString());

        Assert.AreEqual(new TestRecord("hej", 3), decryptedResult.Value!);
    }
}

