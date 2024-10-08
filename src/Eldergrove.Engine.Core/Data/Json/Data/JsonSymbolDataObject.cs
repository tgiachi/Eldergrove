using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Data;

public class JsonSymbolDataObject : IJsonSymbolDataObject
{
    public string Symbol { get; set; }
    public string? Foreground { get; set; }
    public string? Background { get; set; }

    public bool IsBlocking { get; set; }

    public bool IsTransparent { get; set; }
}
