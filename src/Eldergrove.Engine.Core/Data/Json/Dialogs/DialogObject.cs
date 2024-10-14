using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Dialogs;

[DataLoaderType("dialog")]
public class DialogObject : IJsonDataObject
{
    public string Id { get; set; }

    public string? TextId { get; set; }

    public string? Action { get; set; }

    public List<DialogObject> Options { get; set; } = new();
}
