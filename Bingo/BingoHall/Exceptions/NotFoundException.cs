using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BingoHallTests")]
namespace BingoHall.Exceptions;
internal class NotFoundException : Exception
{
    public NotFoundException(string message = "Not found") : base(message)
    {
    }
}