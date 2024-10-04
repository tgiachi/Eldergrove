using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.TileSet;

public class TileEntry : IJsonDataObject, IJsonTagsDataObject
{
    public string Id { get; set; }
    
    public string[]? Tags { get; set; }
}
