using System.Collections.Immutable;

namespace BingoHall.Dtos.Responses;

public record BingoCard
{
    public string Id { get; init; }
    public ImmutableArray<int> B { get; init; }
    public ImmutableArray<int> I { get; init; }
    public ImmutableArray<int> N { get; init; }
    public ImmutableArray<int> G { get; init; }
    public ImmutableArray<int> O { get; init; }
}
