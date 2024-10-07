using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Names;

[DataLoaderType("name")]
public class NamesObject : IJsonDataObject
{
    public string Id { get; set; }

    public string[] Value { get; set; }
}
