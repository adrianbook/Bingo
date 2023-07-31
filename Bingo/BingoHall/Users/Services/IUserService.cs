using BingoHall.DataTransfer;
using BingoHall.Users.Models;

namespace BingoHall.Users.Services;

public interface IUserService
{
    public Task<Result<string>> GetToken(string email, string password);
    Task<Result> CreateUser(BingoUserModel user);
    Task<Result<BingoUserModel>> GetUserByEmail(string email);
}
