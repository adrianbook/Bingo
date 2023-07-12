using Accessories.BingoCard;
using BingoHall.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BingoHall.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BingoCards : ControllerBase
{
    private readonly ICardFactory cardFactory;
    public BingoCards(ICardFactory cardFactory)
    {
        this.cardFactory=cardFactory;
    }

    [HttpGet]
    public BingoCard Get()
    {
        var card = cardFactory.MakeCard();
        var columns = card.Columns;
        return new BingoCard
        {
            Id = card.Id,
            B = columns[0],
            I = columns[1],
            N = columns[2],
            G = columns[3],
            O = columns[4]
        };
    }
}
