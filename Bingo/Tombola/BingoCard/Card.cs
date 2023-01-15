using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessories.BingoCard
{
    public record class Card
    {
        private int[,] numbers;
        public ImmutableList<int> B { get; init; }
        public ImmutableList<int> I { get; init; }
        public ImmutableList<int> N { get; init; }
        public ImmutableList<int> G { get; init; }
        public ImmutableList<int> O { get; init; }

        
    }
}
