using System.Security.Claims;

namespace BingoHall.Authorization.JwtTokens;

public class RequesteeInfo
{
    public string Email { get; internal set; } = null!;
    public List<string>? Roles { get; internal set; } = null!;
}