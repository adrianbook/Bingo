using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DataTransferUtilityTest")]
namespace DataTransfer.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string message = "Not found") : base(message)
    {
    }
}