using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DataTransferUtilityTest")]
namespace DataTransfer.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message = "Bad request") : base(message)
    {
    }
}
