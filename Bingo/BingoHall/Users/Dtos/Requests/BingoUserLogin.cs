namespace BingoHall.Users.Dtos.Requests;

public record BingoUserLogin
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}
