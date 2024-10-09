using System.Text.Json.Serialization;
using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Data.Json.Data;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Json.Maps;

[DataLoaderType("map_fabric")]
public class MapFabricObject : IJsonDataObject, IJsonCategoryObject, IJsonNamedObject
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Category { get; set; }

    public string? SubCategory { get; set; }

    public string[] Fabric { get; set; }

    public JsonSymbolDataObject Wall { get; set; }

    public JsonSymbolDataObject Floor { get; set; }

    public Dictionary<MapLayerType, Dictionary<string, string>> Layers { get; set; }

    [JsonIgnore] public int Area => Width * Height;
    [JsonIgnore] public int Width => Fabric[0].Length;
    [JsonIgnore] public int Height => Fabric.Length;
    [JsonIgnore] public char[][] ToArray => Fabric.Select(x => x.ToCharArray()).ToArray();
}
