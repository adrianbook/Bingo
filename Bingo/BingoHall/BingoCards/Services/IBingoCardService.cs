using BingoHall.BingoCards.Dtos.Responses;

namespace BingoHall;

public interface IBingoCardService
{
    public BingoCard GenerateSingleBingoCard();
}