using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Json.Bars;

[DataLoaderType("bar_def")]
public class BarDefinition : IJsonDataObject
{
    public string Id { get; set; }
    public List<string> BarIds { get; set; }

    public BarPositionType Position { get; set; } = BarPositionType.Top;
}
