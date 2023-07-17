using BingoHall.Dapper;
using BingoHall.Users.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BingoHall.Users;
[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly DapperContext _context;

    public UsersController(DapperContext context)
    {
        _context=context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            using var connection = _context.CreateConnection();

            var user = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM [User]");
            return Ok(user);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
