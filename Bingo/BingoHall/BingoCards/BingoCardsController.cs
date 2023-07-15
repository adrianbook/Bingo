using Accessories.BingoCardCreation;
using BingoHall.BingoCards.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BingoHall.BingoCards;
[ApiController]
[Route("api/[controller]")]
public class BingoCardsController : ControllerBase
{
    private readonly IBingoCardService _cardService;

    public BingoCardsController(IBingoCardService cardService)
    {
        _cardService=cardService;
    }

    [HttpGet]
    public ActionResult<BingoCard> Get()
    {
        var card = _cardService.GenerateSingleBingoCard();
        return card is not null ? Ok(card) : StatusCode(500);
    }
}
