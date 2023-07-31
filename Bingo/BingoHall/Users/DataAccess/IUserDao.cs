using BingoHall.DataTransfer;
using BingoHall.Users.Dtos.Requests;
using BingoHall.Users.Models;
using System.Runtime.CompilerServices;

namespace BingoHall.Users.DataAccess;

public interface IUserDao
{
    Task<Result> CreateUser(BingoUserModel user);
    Task<Result<BingoUserModel>> GetUserByEmail(string email);
}
