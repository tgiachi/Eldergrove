using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.GameObject;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Actions.Npcs;

[SchedulerAction("entity_action")]
public class EntityPerformAction : AbstractSchedulerAction
{
    private readonly Direction _direction;

    private readonly NpcGameObject _entity;


    public EntityPerformAction(Direction direction, NpcGameObject entity)
    {
        _direction = direction;
        _entity = entity;
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        var newPosition = _entity.Position + _direction;
        var entity = _entity.CurrentMap.GetEntityAt<PropGameObject>(newPosition);

        if (entity == null)
        {
            return ActionResult.Fail();
        }

        if (entity.IsDoor)
        {
            entity.Door.Action();

            return ActionResult.Succeed();
        }

        if (entity.CanDestroy)
        {
            return ActionResult.Fail(new EntityPropAttackAction(_entity, entity));
        }

        return ActionResult.Fail();
    }
}
