namespace Accessories.BingoCard
{
    public class CardFactory : ICardFactory
    {
        private Random rand = new Random();

        public Card MakeCard()
        {
            var numbers = new List<int>();
            // make 5 rows consting of 5 random numbers from sequential sets of 15 numbers
            while (numbers.Count < 25)
            {
                int row = numbers.Count / 5;
                int rowSpanStart = (row * 15) + 1;
                int rowSpanEnd = (row * 15) + 16;
                int randomNumber = rand.Next(rowSpanStart, rowSpanEnd);
                if (!numbers.Contains(randomNumber))
                {
                    numbers.Add(randomNumber);
                }
            }
            return new Card(numbers);
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
