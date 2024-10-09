using System.Text.Json.Serialization;
using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Data.Json.Data;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Json.Maps;

[DataLoaderType("map_fabric")]
public class MapFabricObject : IJsonDataObject
{
    public string Id { get; set; }
    public char[][] Fabric { get; set; }

    public JsonSymbolDataObject Wall { get; set; }

    public JsonSymbolDataObject Floor { get; set; }

    public Dictionary<MapLayerType, string[][]> Layers { get; set; }

    [JsonIgnore] public int Area => Width * Height;
    [JsonIgnore] public int Width => Fabric[0].Length;
    [JsonIgnore] public int Height => Fabric.Length;
}
