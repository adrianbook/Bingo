namespace DataTransferUtility.JwtTokens;

public interface IJwtTokenGenerator
{
    public string GenerateToken(RequesteeInfo info);
}
