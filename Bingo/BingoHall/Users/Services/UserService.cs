using BingoHall.Users.DataAccess;
using BingoHall.Users.Models;
using DataTransfer.Exceptions;
using DataTransferUtility;
using DataTransferUtility.JwtTokens;
using PasswordsAndEncryption.Passwords;

namespace BingoHall.Users.Services;

public class UserService : IUserService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserDao _userDao;
    public UserService(IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher, IUserDao dao)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userDao = dao;
    }

    public Task<Result> CreateUser(BingoUserModel user)
    {
        return _userDao.CreateUser(user);
    }

    public async Task<Result<string>> GetToken(string email, string password)
    {
        var userResult = await _userDao.GetUserByEmail(email);
        if (userResult.IsFailure) { return new NotFoundException($"User with email {email} not found"); }
        var user = userResult.Value!;

        if (!_passwordHasher.PasswordMatchesHash(password, user.PasswordHash!))
        {
            return new BadRequestException("Invalid password");
        }

        var token = _jwtTokenGenerator.GenerateToken(new() { Email = user.Email, Roles = user.Roles?.Select(r => r.Label).ToList() });

        return token;
    }

    public Task<Result<BingoUserModel>> GetUserByEmail(string email)
    {
        return _userDao.GetUserByEmail(email);
    }
}
