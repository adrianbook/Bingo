using PasswordsAndEncryption.Passwords;

namespace PasswordsAndEncryptionTest;

[TestClass]
public class PasswordHashingTests
{
    [TestMethod]
    public void TestHashing()
    {
        var hasher = new PasswordHasher();
        var password = "password";

        var hash = hasher.GenerateHash(password);

        Assert.IsTrue(hasher.PasswordMatchesHash("password", hash));
    }

    [TestMethod]
    public void TestHashíngLongPassword()
    {
        var hasher = new PasswordHasher();
        var password = "passwordpasswordpasswordpasswo";
        var hash = hasher.GenerateHash(password);

        Assert.IsTrue(hasher.PasswordMatchesHash(password, hash));
    }
}