namespace BingoHall.Authorization.JwtTokens.Config;

public interface IJwtTokenGeneratorConfiguration
{
    public string Key { get; }
    public string Issuer { get; }
    public string Audience { get; }
}
