namespace Accessories.BingoCardCreation;
public interface ICardDataFactory
{
    CardData MakeCard();
    IEnumerable<CardData> MakeCards(int count);

}
