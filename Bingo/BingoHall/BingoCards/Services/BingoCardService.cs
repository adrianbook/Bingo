using Accessories.BingoCardCreation;
using BingoHall.BingoCards.Dtos.Responses;
using BingoHall.Encryption;

namespace BingoHall;

internal class BingoCardService : IBingoCardService
{
    private readonly ICardDataFactory _cardFactory;

    public BingoCardService(ICardDataFactory cardDataFactory)
    {
        _cardFactory = cardDataFactory;
    }

    public BingoCard GenerateSingleBingoCard()
    {
        var rawCard = _cardFactory.MakeCard();
        var byteArrayNumbers = rawCard.Numbers.Select(n => (byte) n).ToArray();
        var encryptedId = Encrypter.EncryptByteArray(byteArrayNumbers, "password", new byte[] {2,3,1,14});
        if (encryptedId == null)
        {
            throw new ArgumentNullException(nameof(encryptedId));
        }
        return new()
        {
            Id = encryptedId,
            B = rawCard.Columns[0],
            I = rawCard.Columns[1],
            N = rawCard.Columns[2],
            G = rawCard.Columns[3],
            O = rawCard.Columns[4]
        };
    }
}