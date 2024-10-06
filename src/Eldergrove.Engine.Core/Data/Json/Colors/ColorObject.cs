using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Colors;

[DataLoaderType("color")]
public class ColorObject : IJsonDataObject
{
    public string Id { get; set; }

    public int[] Value { get; set; }
}
