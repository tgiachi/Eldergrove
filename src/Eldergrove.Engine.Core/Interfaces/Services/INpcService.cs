using Eldergrove.Engine.Core.Ai;
using Eldergrove.Engine.Core.Data.Json.Npcs;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.GameObjects;


namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface INpcService : IGameObjectFactory<NpcGameObject>
{
    void AddNpc(NpcObject npc);

    void AddBrain(string id, Func<AiContext, List<ISchedulerAction>> brain);

    IEnumerable<ISchedulerAction> InvokeBrain(string id, AiContext context);
}
