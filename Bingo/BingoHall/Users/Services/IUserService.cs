using BingoHall.Users.Models;
using DataTransferUtility;

namespace BingoHall.Users.Services;

public interface IUserService
{
    public Task<Result<string>> GetToken(string email, string password);
    Task<Result> CreateUser(BingoUserModel user);
    Task<Result<BingoUserModel>> GetUserByEmail(string email);
}
