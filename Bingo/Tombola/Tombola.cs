using Accessories.Extensions;
using System.Collections.Immutable;

namespace Accessories
{
    public class Tombola : ITombola
    {
        private Stack<int> NumbersInTombola;
        private HashSet<int> DrawnNumbers;

        public Tombola()
        {
            var shuffledNumbers = Enumerable.Range(1, 75).ToList().ShuffleAndReturn();
            NumbersInTombola = new Stack<int>(shuffledNumbers);
            DrawnNumbers = new HashSet<int>();
        }
        public int NextNumber()
        {
            if (NumbersInTombola.Count == 0)
            {
                return 0;
            }
            else
            {
                int drawnNumber = NumbersInTombola.Pop();
                if (!DrawnNumbers.Add(drawnNumber))
                {
                    throw new Exception("Tombola contains duplicates");
                }
                return drawnNumber;
            }
        }

        public ImmutableList<int> GetDrawnNumbers()
        {
            return DrawnNumbers.ToList().OrderBy(x => x).ToImmutableList();
        }
    }

}
