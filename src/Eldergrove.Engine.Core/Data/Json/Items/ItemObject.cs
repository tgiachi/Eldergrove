using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Items;

[DataLoaderType("item")]
public class ItemObject : IJsonDataObject, IJsonSymbolDataObject, IJsonTagsDataObject, IJsonCategoryObject, IJsonNamedObject
{
    public string Id { get; set; }
    public string Symbol { get; set; }
    public string? Foreground { get; set; }
    public string? Background { get; set; }


    public string[]? Tags { get; set; }
    public string Category { get; set; }

    public string? SubCategory { get; set; }
    public string Name { get; set; }

    public List<ItemFeatureObject>? Features { get; set; }
}
