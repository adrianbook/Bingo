using Accessories.BingoCardCreation;
using BingoHall.BingoCards.Dtos.Responses;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace BingoHall.BingoCards;
[ApiController]
[Route("api/[controller]")]
public class BingoCardsController : Controller
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
        var hashids = new Hashids("salt");

        return card is not null ? Ok(card) : StatusCode(500);
    }
}
