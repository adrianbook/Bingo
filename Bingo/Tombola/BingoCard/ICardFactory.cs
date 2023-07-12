namespace Accessories.BingoCard;
public interface ICardFactory
{
    Card MakeCard();
    IEnumerable<Card> MakeCards(int count);

}
