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
        public string Id { get; init; }
        public ImmutableList<ImmutableList<int>> Rows { get; init; }

        internal Card(List<int> numbers)
        {
            Id = numbers.Select(n => n < 10 ? "0"+n.ToString(): n.ToString()).Aggregate((prev, cur)=> prev + cur);
            var topList = new List<ImmutableList<int>>();
            for (int i = 0; i < 5; i++) {
                topList.Add(new int[] { numbers[i], numbers[i + 5], numbers[i+10], numbers[i+15], numbers[i+20]}.ToImmutableList());
            }
            Rows = topList.ToImmutableList();
        }
    }
}
