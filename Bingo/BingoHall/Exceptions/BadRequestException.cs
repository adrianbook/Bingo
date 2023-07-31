using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BingoHallTests")]
namespace BingoHall.Exceptions;

internal class BadRequestException : Exception
{
    public BadRequestException(string message = "Bad request") : base(message)
    {
    }
}
