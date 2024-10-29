using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Json.Npcs;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.GameObjects;
using Eldergrove.Engine.Core.Maps;
using SadRogue.Primitives;


namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface INpcService : IGameObjectFactory<NpcGameObject>
{

    PlayerGameObject Player { get; set; }


    void BuildPlayer(GameMap map);

    PlayerGameObject GetPlayer();

    void AddNpc(NpcObject npc);

    void AddBrain(string id, Func<AiContext, List<ISchedulerAction>> brain);

    IEnumerable<ISchedulerAction> InvokeBrain(string id, AiContext context);
}
