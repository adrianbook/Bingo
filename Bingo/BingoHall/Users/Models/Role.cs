namespace BingoHall.Users.Models;

public record Role
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
}
