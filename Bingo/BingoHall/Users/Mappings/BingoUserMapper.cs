using BingoHall.Users.Dtos.Requests;
using BingoHall.Users.Models;
using PasswordsAndEncryption.Passwords;

namespace BingoHall.Users.Mappings;

public class BingoUserMapper
{
    private readonly IPasswordHasher _passwordHasher;

    public BingoUserMapper(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public BingoUserModel? MapNewUserToModel(BingoUserNew user)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Email))
            {
                return null;
            }
            var hashedPassword = _passwordHasher.GenerateHash(user.Password!);

            return new BingoUserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = hashedPassword
            };
        }
        catch(Exception ex)
        {
            return null;
        }
    }


}
