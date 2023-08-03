using Microsoft.AspNetCore.Mvc;

namespace BingoHall.Accounts;
public class AccountsController : Controller
{
    [HttpGet("/{pw}")]
    public IActionResult UpdatePassword([FromRoute] string pw)
    {
        ViewData["password"] = pw;
        return View();
    }
}
