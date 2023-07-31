namespace BingoHall.Users.Models;

public record BingoUserModel
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string Email { get; init; } = null!;
    public string? PasswordHash { get; init; }

    public virtual ICollection<RoleModel>? Roles { get; init; }
}
