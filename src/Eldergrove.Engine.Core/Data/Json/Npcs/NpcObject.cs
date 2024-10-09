using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Npcs;

[DataLoaderType("npc")]
public class NpcObject : IJsonDataObject, IJsonTagsDataObject, IJsonNamedObject, IJsonSymbolDataObject, IJsonCategoryObject
{
    public string Id { get; set; }

    public string[]? Tags { get; set; }

    public string Name { get; set; }

    public string BrainAi { get; set; }
    public string Symbol { get; set; }
    public string? Foreground { get; set; }
    public string? Background { get; set; }

    public bool IsBlocking { get; set; } = true;

    public bool IsTransparent { get; set; } = false;
    public string Category { get; set; }
    public string? SubCategory { get; set; }
}
