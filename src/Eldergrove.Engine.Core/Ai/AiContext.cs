using Eldergrove.Engine.Core.Maps;

namespace Eldergrove.Engine.Core.Ai;

public class AiContext
{
    public GameMap Map { get; set; }


    public override string ToString() => $"AiContext:";
}
