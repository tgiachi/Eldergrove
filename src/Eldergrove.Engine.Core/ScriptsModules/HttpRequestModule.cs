using System.Text.Json;
using Eldergrove.Engine.Core.Attributes.Scripts;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class HttpRequestModule
{
    [ScriptFunction("http_get", "Makes a GET request to the specified URL and returns the response.")]
    public string HttpGet(string url)
    {
        using var httpClient = new HttpClient();

        var response = httpClient.GetAsync(url).GetAwaiter().GetResult();

        return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    }

    [ScriptFunction("from_json", "Converts a JSON string to an object.")]
    public object JsonToObject(string json)
    {
        return JsonSerializer.Deserialize<object>(json);
    }
}
