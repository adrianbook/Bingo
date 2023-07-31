namespace BingoHall.Users.Dtos.Requests;

public record BingoUserNew
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string Email { get; init; } = null!;
    public string? Password { get; init; }
}
