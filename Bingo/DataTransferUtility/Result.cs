using DataTransfer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace DataTransferUtility;

public record Result
{
    public Exception? Failure { get; init; }
    public bool IsSuccess => Failure is null;
    public bool IsFailure => !IsSuccess;
    protected Result(Exception failure)
    {
        Failure = failure;
    }
    protected Result()
    {
    }

    public static implicit operator Result(Exception failure) => new(failure);
    public TOut Resolve<TOut>(Func<TOut> onSuccess, Func<Exception, TOut> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Failure!);
    }

    public IActionResult ResolveAsIActionResult(string? notFound = null, string? badRequest = null)
      => Resolve<IActionResult>(
             () => new NoContentResult(),
             (ex) => ex switch
             {
                 NotFoundException => new NotFoundObjectResult(notFound ?? "Requested resource not found"),
                 BadRequestException => new BadRequestObjectResult(badRequest ?? "Bad request"),
                 _ => new StatusCodeResult(500)
             }

                       );
    public static Result Success => new();
}

public record Result<TSuccess> : Result
{
    public TSuccess? Value { get; init; }

    protected Result(TSuccess value)
    {
        Value = value;
    }

    protected Result(Exception ex) : base(ex) { }

    public static implicit operator Result<TSuccess>(TSuccess value) => new(value);
    public static implicit operator Result<TSuccess>(Exception failure) => new(failure);

    public TOut Resolve<TOut>(Func<TSuccess, TOut> onSuccess, Func<Exception, TOut> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Failure!);
    }

    public new IActionResult ResolveAsIActionResult(string? notFound = null, string? badRequest = null)
        => Resolve<IActionResult>(
               (val) => new OkObjectResult(val),
               (ex) => ex switch
                   {
                       NotFoundException => new NotFoundObjectResult(notFound ?? "Requested resource not found"),
                       BadRequestException => new BadRequestObjectResult(badRequest ?? "Bad request"),
                       _ => new StatusCodeResult(500)
                   }
               
                         );

}