using BingoHall.Dapper;
using BingoHall.DataTransfer;
using BingoHall.Exceptions;
using BingoHall.Users.Dtos.Requests;
using BingoHall.Users.Mappings;
using BingoHall.Users.Models;
using Dapper;
using PasswordsAndEncryption.Passwords;

namespace BingoHall.Users.DataAccess;

public class UserDao : IUserDao
{
    private readonly DapperContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public UserDao(DapperContext context, IPasswordHasher passwordHasher)
    {
        _context=context;
        _passwordHasher=passwordHasher;
    }

    public async Task<Result> CreateUser(BingoUserModel user)
    {
        var sql = @"
                    INSERT INTO [dbo].[BingoUser]
                        (FirstName, LastName, Email, PasswordHash) 
                    VALUES
                        (@FirstName, @LastName, @Email, @PasswordHash)
                    ";
        try
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, user);

            return Result.Success;
        }
        catch(Exception ex)
        {
            return ex;
        }
    }

    public async Task<Result<BingoUserModel>> GetUserByEmail(string email)
    {
        try
        {
            var sql = @"
                SELECT
                    u.Id, u.FirstName, u.LastName, u.Email, u.PasswordHash, r.Id as RoleId, r.Label FROM [dbo].[BingoUser] as u
                LEFT JOIN [dbo].[BingoUserRole] as ur
                    ON ur.BingoUserId = u.Id
                LEFT JOIN [dbo].[Role] as r
                    ON r.Id = ur.RoleId
                WHERE u.Email = @email
                ";

            using var connection = _context.CreateConnection();
            var queryResult = await connection.QueryAsync(
                sql,
                new { email });
            if (queryResult is null)
            {
                return new NotFoundException();
            }

            var first = queryResult.First();

            var user = new BingoUserModel() {
                Id = first.Id,
                FirstName = first.FirstName,
                LastName = first.LastName,
                Email = first.Email,
                PasswordHash = first.PasswordHash,
                Roles = first.RoleId is not null ? queryResult.Select(r => new RoleModel() { Id = r.RoleId, Label = r.Label }).ToList() : null
            };
            
           
            return user;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<Result> UpdateUser(BingoUserModel user)
    {
        var sql = @"
                    UPDATE [dbo].[BingoUser]
                    SET
                        FirstName = @FirstName,
                        LastName = @LastName,
                        Email = @Email,
                        PasswordHash = @PasswordHash
                    WHERE Id = @Id
                    ";
        try
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, user);

            return Result.Success;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
