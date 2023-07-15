namespace Accessories.BingoCardCreation
{
    public class CardDataFactory : ICardDataFactory
    {
        private Random rand = new Random();

        public CardData MakeCard()
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
            return new CardData(numbers);
        }

        public IEnumerable<CardData> MakeCards(int count)
        {
            var result = new List<CardData>();
            for (int i = 0; i < count; i++)
            {
                result.Add(MakeCard());
            }
            return result;
        }
    }
}
