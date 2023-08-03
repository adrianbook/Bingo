namespace DataTransferUtility.JwtTokens.Config;

public class JwtTokenGeneratorConfigurationBase : IJwtTokenGeneratorConfiguration
{
    public JwtTokenGeneratorConfigurationBase(string key, string issuer, string audience)
    {
        Key = key;
        Issuer = issuer;
        Audience = audience;
    }
    public string Key { get; }

    public string Issuer { get; }

    public string Audience { get; }
}
