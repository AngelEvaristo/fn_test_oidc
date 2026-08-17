using Amazon.Lambda.Core;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace sesion5_github_lf_2026;

public class Function
{
    
    /// <summary>
    /// Accepts either a JSON string ("hello") or an object payload ({"input":"hello"}) and returns uppercase.
    /// </summary>
    public string FunctionHandler(JsonElement input, ILambdaContext context)
    {
        string? value = null;

        if (input.ValueKind == JsonValueKind.String)
        {
            value = input.GetString();
        }
        else if (input.ValueKind == JsonValueKind.Object &&
                 input.TryGetProperty("input", out JsonElement inputValue) &&
                 inputValue.ValueKind == JsonValueKind.String)
        {
            value = inputValue.GetString();
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            context.Logger.LogLine("Invalid payload. Expected JSON string or object with an 'input' field.");
            throw new ArgumentException("Invalid payload. Use \"hello\" or {\"input\":\"hello\"}.");
        }

        return value.ToUpperInvariant();
    }
}
