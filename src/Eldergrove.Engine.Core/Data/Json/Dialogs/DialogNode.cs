using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Dialogs;

public class DialogNode : IJsonDataObject
{
    public string Id { get; set; }

    public string Text { get; set; }

    public List<DialogOption> Options { get; set; } = new();
}
