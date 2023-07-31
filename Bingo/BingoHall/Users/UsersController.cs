using BingoHall.Dapper;
using BingoHall.Users.Dtos.Requests;
using BingoHall.Users.Models;
using BingoHall.Users.DataAccess;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using BingoHall.Users.Mappings;
using BingoHall.Users.Services;

namespace BingoHall.Users;
[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly BingoUserMapper _mapper;
    public UsersController(IUserService userService, BingoUserMapper bingoUserMapper)
    {
        _userService = userService;
        _mapper = bingoUserMapper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddUser([FromBody] BingoUserNew newUser)
    {

        var addedUser = await _userService.CreateUser(_mapper.MapNewUserToModel(newUser)!);
        if (addedUser is null) { return BadRequest();}
        return Ok(addedUser);
    }

    [HttpGet("/{email}")]
    public async Task<IActionResult> Login([FromRoute] string email)
    {
        var result = await _userService.GetUserByEmail(email);
        return result.ResolveAsIActionResult();
    }

    [HttpGet("/aquiretoken")]
    public async Task<IActionResult> AquireToken([FromQuery] string email, [FromQuery] string password)
    {
        var result = await _userService.GetToken(email, password);
        return result.ResolveAsIActionResult();
    }

}
