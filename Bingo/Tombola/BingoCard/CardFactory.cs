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
            var numbers = new HashSet<int>();

            for(int i = 0; i < 5; i++)
            {
                while (numbers.Count < (i+1)*5) {
                    numbers.Add(rand.Next(15)+1+i*15);
                }
            }
            return new Card(numbers.ToList());
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
