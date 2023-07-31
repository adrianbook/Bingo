namespace BingoHall.Users.Models;

public record RoleModel
{
    public Guid Id { get; init; }
    public string Label { get; init; } = null!;
}
