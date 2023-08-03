using System.Security.Claims;

namespace DataTransferUtility.JwtTokens;

public record RequesteeInfo
{
    public string Email { get; init; } = null!;
    public List<string>? Roles { get; init; } = null!;
}