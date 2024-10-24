using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Data.Json.Data;
using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Npcs;

[DataLoaderType("npc")]
public class NpcObject
    : IJsonDataObject,
        IJsonTagsDataObject,
        IJsonNamedObject,
        IJsonSymbolDataObject,
        IJsonCategoryObject,
        IJsonContainerObject
{
    public string Id { get; set; }

    public string[]? Tags { get; set; }

    public string Name { get; set; }

    public string BrainAi { get; set; }
    public string Symbol { get; set; }
    public string? Foreground { get; set; }
    public string? Background { get; set; }

    public string Category { get; set; }

    public string? SubCategory { get; set; }

    public string AiBrain { get; set; }

    public JsonSkillsObject Skills { get; set; }

    public List<JsonRandomObject>? SellableItems { get; set; }

    public List<JsonRandomObject>? Container { get; set; }
}
