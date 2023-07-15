namespace Accessories.BingoCard;
public interface ICardDataFactory
{
    CardData MakeCard();
    IEnumerable<CardData> MakeCards(int count);

}
