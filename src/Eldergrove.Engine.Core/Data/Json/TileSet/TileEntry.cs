using System.ComponentModel.DataAnnotations;
using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.TileSet;

[DataLoaderType("tile")]
public class TileEntry : IJsonDataObject, IJsonTagsDataObject, IJsonSymbolDataObject, IJsonCategoryObject
{
    public string Id { get; set; }

    public string[]? Tags { get; set; }

    // Symbol can be a single character or
    // id of a sprite in a sprite sheet starting with #
    // or animation like @id-id
    public string Symbol { get; set; }
    public string? Foreground { get; set; } = "#FFFFFF";
    public string? Background { get; set; } = "#FFFFFF";

    public bool IsBlocking { get; set; }

    public bool IsTransparent { get; set; }

    public TileAnimationObject? Animation { get; set; }

    public string Category { get; set; }

    public string? SubCategory { get; set; }
}
