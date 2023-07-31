namespace BingoHall.Authorization.JwtTokens;

public interface IJwtTokenGenerator
{
    public string GenerateToken(RequesteeInfo info);
}
