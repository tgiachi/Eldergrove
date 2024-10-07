using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Props;

[DataLoaderType("prop")]
public class PropObject : IJsonDataObject, IJsonSymbolDataObject
{
    public string Id { get; set; }

    public string Category { get; set; }

    public string Symbol { get; set; }

    public string? Foreground { get; set; }

    public string? Background { get; set; }

    public bool IsBlocking { get; set; }

    public bool IsTransparent { get; set; }

    public bool IsDestructible { get; set; }
}
