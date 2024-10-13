using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Json.Npcs;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.GameObjects;
using SadRogue.Primitives;


namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface INpcService : IGameObjectFactory<NpcGameObject>
{

    PlayerGameObject Player { get; set; }


    void BuildPlayer(Point position);

    PlayerGameObject GetPlayer();

    void AddNpc(NpcObject npc);

    void AddBrain(string id, Func<AiContext, List<ISchedulerAction>> brain);

    IEnumerable<ISchedulerAction> InvokeBrain(string id, AiContext context);
}
