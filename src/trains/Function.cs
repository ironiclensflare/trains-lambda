using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace trains;

public class Function
{
    public async Task<string> FunctionHandler(TrainRequest request, ILambdaContext context)
    {
        var client = new TrainClient.TrainClient();
        var response = await client.GetTrains(request.Crs);
        return response;
    }
}

public class TrainRequest
{
    public string Crs { get; set; }
}
