using Eldergrove.Engine.Core.Data.Json.Random;

namespace Eldergrove.Engine.Core.Data.Game;

public class PlayerConfig
{
    public JsonRandomObject StartingGold { get; set; }

    public List<JsonRandomObject> StartingItems { get; set; } = new();
}
