namespace BingoHall.Authorization.JwtTokens.Config;

public class JwtTokenGeneratorConfigurationDI : JwtTokenGeneratorConfigurationBase
{
    public JwtTokenGeneratorConfigurationDI(IConfiguration configuration) : base(
            configuration["Jwt:Key"],
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"])
    {
    }
}
