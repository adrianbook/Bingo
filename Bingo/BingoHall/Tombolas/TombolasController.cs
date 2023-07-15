using BingoHall.Tombolas.Services;
using Microsoft.AspNetCore.Mvc;

namespace BingoHall.Tombolas;
public class TombolasController : Controller
{
    private readonly ITombolaService _tombolaService;

    public TombolasController(ITombolaService tombolaService)
    {
        this._tombolaService=tombolaService;
    }

    [HttpPost("new")]
    public ActionResult<string> Index()
    {
        return _tombolaService.RegisterTombola();
    }

    [HttpPut("nextNumber/{tombolaId}")]
    public ActionResult<int> NextNumber([FromRoute] string tombolaId)
    {
        return _tombolaService.GetNextNumber(tombolaId);
    }
}
