using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Data;

public class JsonStateObject : IJsonStateObject
{
    public JsonSymbolDataObject On { get; set; }

    public JsonSymbolDataObject Off { get; set; }
}
