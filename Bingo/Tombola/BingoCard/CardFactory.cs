using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessories.BingoCard
{
    public class CardFactory
    {
        private Random rand = new Random();

        public Card MakeCard()
        {
            var numbers = new List<HashSet<int>>();

            for(int i = 0; i < 5; i++)
            {
                numbers[i] = new HashSet<int>();
                while (numbers[i].Count < 5) {
                    numbers[i].Add(rand.Next(15)+1+i*15);
                }
            }
            return new Card()
            {
                B = numbers[0].ToImmutableList(),
                I = numbers[1].ToImmutableList(),
                N = numbers[2].ToImmutableList(),
                G = numbers[3].ToImmutableList(),
                O = numbers[4].ToImmutableList(),
            };
        }

        public IEnumerable<Card> MakeCards(int count)
        {
            var result = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                result.Add(MakeCard());
            }
            return result;
        }
    }
}
