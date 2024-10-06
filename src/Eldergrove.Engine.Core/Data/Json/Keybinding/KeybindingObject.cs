using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Keybinding;

[DataLoaderType("keybinding")]
public class KeybindingObject : IJsonDataObject
{
    public string Id { get; set; }

    public string Key { get; set; }

    public string Command { get; set; }
}
