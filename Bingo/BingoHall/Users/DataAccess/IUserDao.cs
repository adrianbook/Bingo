using BingoHall.Users.Models;
using DataTransferUtility;

namespace BingoHall.Users.DataAccess;

public interface IUserDao
{
    Task<Result> CreateUser(BingoUserModel user);
    Task<Result<BingoUserModel>> GetUserByEmail(string email);
    Task<Result> UpdateUser(BingoUserModel user);
}
