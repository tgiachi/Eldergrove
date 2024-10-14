using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Texts;

[DataLoaderType("text")]
public class TextObject : IJsonDataObject
{
    public string Id { get; set; }

    public string Text { get; set; }
}
