using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Data.Json.Data;
using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Props;

[DataLoaderType("prop")]
public class PropObject : IJsonDataObject, IJsonSymbolDataObject, IJsonCategoryObject, IJsonNamedObject, IJsonContainerObject
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string Category { get; set; }

    public string? SubCategory { get; set; }
    public string Symbol { get; set; }
    public string? Foreground { get; set; }
    public string? Background { get; set; }
    public bool IsDestructible { get; set; }
    public JsonRandomObject? DestroyHealth { get; set; }
    public List<JsonRandomObject>? OnDestroy { get; set; }
    public JsonStateObject? Door { get; set; }
    public List<JsonRandomObject>? Container { get; set; }
    public JsonPortalObject? Portal { get; set; }
}
