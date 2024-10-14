using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Json.Bars;

[DataLoaderType("bar_definition")]
public class BarDefinition : IJsonDataObject
{
    public string Id { get; set; }
    public List<string> BarsIds { get; set; }

    public BarPositionType Position { get; set; }
}
