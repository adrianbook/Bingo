using System.Collections.Immutable;

namespace Accessories.TombolaCreation;
public interface ITombola
{
    public int NextNumber();

    public ImmutableList<int> GetDrawnNumbers();
}
