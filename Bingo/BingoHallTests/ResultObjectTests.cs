using BingoHall.DataTransfer;
using BingoHall.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BingoHallTests;

[TestClass]
public class ResultObjectTests
{
    [TestMethod]
    public void TestResultResolve()
    {
        var result = CreateGenericsResult(new NotFoundException());

        var resolved = result.ResolveAsIActionResult(notFound: "Inte hitta");
        Assert.IsNotNull(resolved);
        Assert.IsInstanceOfType(resolved, typeof(NotFoundObjectResult));
        Assert.AreEqual("Inte hitta", ((NotFoundObjectResult)resolved).Value);
    }

    [TestMethod]
    public void TestResultResolveWithNoException()
    {
        var result = CreateGenericsResult(null);
        var resolved = result.ResolveAsIActionResult(notFound: "Inte hitta");
        Assert.IsNotNull(resolved);
        Assert.IsInstanceOfType(resolved, typeof(OkObjectResult));
        Assert.AreEqual("Success", ((OkObjectResult)resolved).Value);
    }

    [TestMethod]
    public void TestResultResolveWithDefaultString()
    {
        var result = CreateGenericsResult(new BadRequestException());
        var resolved = result.ResolveAsIActionResult();
        Assert.IsNotNull(resolved);
        Assert.IsInstanceOfType(resolved, typeof(BadRequestObjectResult));
        Assert.AreEqual("Bad request", ((BadRequestObjectResult)resolved).Value);
    }

    [TestMethod]
    public void TestBasicResultResolve()
    {
        var result = CreateBasicResult(new NotFoundException());
        var resolved = result.ResolveAsIActionResult(notFound: "Inte hitta");
        Assert.IsNotNull(resolved);
        Assert.IsInstanceOfType(resolved, typeof(NotFoundObjectResult));
        Assert.AreEqual("Inte hitta", ((NotFoundObjectResult)resolved).Value);
    }

    [TestMethod]
    public void TestBasicResultResolveWithNoException()
    {
        var result = CreateBasicResult(null);
        var resolved = result.ResolveAsIActionResult(notFound: "Inte hitta");
        Assert.IsNotNull(resolved);
        Assert.IsInstanceOfType(resolved, typeof(NoContentResult));
    }

    [TestMethod]
    public void TestBasicResultResolveWithDefaultString()
    {
        var result = CreateBasicResult(new BadRequestException());
        var resolved = result.ResolveAsIActionResult();
        Assert.IsNotNull(resolved);
        Assert.IsInstanceOfType(resolved, typeof(BadRequestObjectResult));
        Assert.AreEqual("Bad request", ((BadRequestObjectResult)resolved).Value);
    }

    private Result CreateBasicResult(Exception? exceptionToFailWith)
    {
        return exceptionToFailWith is null ? Result.Success : exceptionToFailWith;
    }

    private Result<string> CreateGenericsResult(Exception? exceptionToFailWith)
    {
        return exceptionToFailWith is null ? "Success" : exceptionToFailWith;
    }
}