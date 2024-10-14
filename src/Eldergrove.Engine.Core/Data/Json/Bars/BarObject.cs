using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Bars;

[DataLoaderType("bar_obj")]
public class BarObject : IJsonDataObject
{
    public string Id { get; set; }

    public string Text { get; set; }

    public string Foreground { get; set; }

    public string Background { get; set; }
}
