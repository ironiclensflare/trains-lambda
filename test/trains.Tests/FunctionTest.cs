using Xunit;
using Amazon.Lambda.TestUtilities;

namespace trains.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestToUpperFunction()
    {
        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var request = new TrainRequest { Crs = "MAN" };
        var response = await function.FunctionHandler(request, context);
        Assert.Equal("Stoke-on-Trent", response);
    }
}
