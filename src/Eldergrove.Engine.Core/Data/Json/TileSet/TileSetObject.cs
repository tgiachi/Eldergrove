using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.TileSet;

public class TileSetObject : IJsonDataObject
{
    public string Id { get; set; }

    public List<TileEntry> Tiles { get; set; } = new();
}
