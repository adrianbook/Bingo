using DataTransferUtility.JwtTokens;
using DataTransferUtility.JwtTokens.Config;

namespace BingoHallTests;
[TestClass]
public class TokenTests
{
    [TestMethod]
    public void TestTokenCreation()
    {
        var tokenGenerator = new JwtTokenGenerator(
            new JwtTokenGeneratorConfigurationBase(
                    key: "1234567890123456",
                    issuer: "BingoHall",
                    audience: "BingoHall"
                )
            );

        var token = tokenGenerator.GenerateToken(new RequesteeInfo
        {
            Email = "adrian@exsolve.se",
            Roles = new List<string> { "Admin", "User" }
        }
        );

        Assert.IsNotNull(token);
        
    }
}
