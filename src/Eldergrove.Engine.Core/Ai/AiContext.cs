using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Maps;

namespace Eldergrove.Engine.Core.Ai;

public class AiContext
{
    public GameMap Map { get; set; }


    public override string ToString() => $"AiContext:";



    public List<ISchedulerAction> EmptyActionList() => new();
}
