using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Dialogs;

[DataLoaderType("dialog")]
public class DialogObject : IJsonDataObject
{
    public string Id { get; set; }

    public string Text { get; set; }

    public List<DialogObject> Options { get; set; } = new();

    public DialogObject(string id, string text)
    {
        Id = id;
        Text = text;
    }
}
