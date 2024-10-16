using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.GameObject;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Actions.Npcs;

[SchedulerAction("entity_movement")]
public class EntityMovementAction : AbstractSchedulerAction
{
    private readonly Direction _direction;
    private readonly NpcGameObject _entity;

    public EntityMovementAction(Direction direction, NpcGameObject entity)
    {
        _direction = direction;

        _entity = entity;
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        var newPosition = _entity.Position + _direction;
        if (_entity.CurrentMap.GameObjectCanMove(_entity, newPosition))
        {
            _entity.Position = newPosition;


            return ActionResult.Succeed();
        }

        var entity = _entity.CurrentMap.GetEntityAt<PropGameObject>(newPosition);

        if (entity != null)
        {
            return ActionResult.Fail(new EntityPerformAction(_direction, _entity));
        }

        return ActionResult.Fail();
    }
}