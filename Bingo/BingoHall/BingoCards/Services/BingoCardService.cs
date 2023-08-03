using Accessories.BingoCardCreation;
using BingoHall.BingoCards.Dtos.Responses;
using HashidsNet;
using PasswordsAndEncryption.Encryption;

namespace BingoHall;

internal class BingoCardService : IBingoCardService
{
    private readonly ICardDataFactory _cardFactory;
    private readonly IEncryptionService _encryptionService;

    public BingoCardService(ICardDataFactory cardDataFactory, IEncryptionService encryptionService)
    {
        _cardFactory = cardDataFactory;
        _encryptionService = encryptionService;
    }

    public BingoCard GenerateSingleBingoCard()
    {
        var rawCard = _cardFactory.MakeCard();
        var byteArrayNumbers = rawCard.Numbers.Select(n => (byte) n).ToArray();
        var encryptedIdResult = _encryptionService.Encrypt(byteArrayNumbers);

        if (encryptedIdResult.IsFailure)
        {
            throw encryptedIdResult.Failure!;
        }
        return new()
        {
            Id = encryptedIdResult.Value!,
            B = rawCard.Columns[0],
            I = rawCard.Columns[1],
            N = rawCard.Columns[2],
            G = rawCard.Columns[3],
            O = rawCard.Columns[4]
        };
    }
}