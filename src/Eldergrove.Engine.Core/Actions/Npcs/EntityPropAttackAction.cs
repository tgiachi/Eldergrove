using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Utils;

namespace Eldergrove.Engine.Core.Actions.Npcs;

[SchedulerAction("entity_npc_attack")]
public class EntityPropAttackAction : AbstractSchedulerAction
{
    public NpcGameObject Source { get; }

    public PropGameObject Target { get; }


    public EntityPropAttackAction(NpcGameObject source, PropGameObject target)
    {
        Source = source;
        Target = target;
    }


    public override async Task<ActionResult> ExecuteAsync()
    {
        if (Target.Health != null)
        {
            //Target.Health.TakeDamage( Source.Damage);

            Target.Health.TakeDamage(new JsonRandomObject("1d3").GetRandomValue());
        }

        return ActionResult.Succeed();
    }
}
