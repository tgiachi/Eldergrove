using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Data.Json.Data;
using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Json.Maps;

[DataLoaderType("map_generator")]
public class MapGeneratorObject : IJsonDataObject
{
    public string Id { get; set; }

    public MapGeneratorType GeneratorType { get; set; }

    public List<JsonRandomObject> Fabrics { get; set; }


    public MapGridObject? Grid { get; set; }

    public JsonSymbolDataObject Wall { get; set; }

    public JsonSymbolDataObject Floor { get; set; }
}
