using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.GameObject;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Actions.Npcs;

[SchedulerAction("search_for")]
public class SearchForAction : AbstractSchedulerAction
{
    private readonly NpcGameObject _entity;

    private readonly int _radius;

    public SearchForAction(NpcGameObject entity, int radius = 1)
    {
        _entity = entity;
        _radius = radius;
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        var searchArea = Radius.Circle.PositionsInRadius(_entity.Position, _radius);

        foreach (var position in searchArea)
        {
            var entity = _entity.CurrentMap.GetEntityAt<PropGameObject>(position);
            if (entity != null)
            {
                return ActionResult.Fail(
                    new EntityPerformAction(Direction.GetDirection(_entity.Position, position), _entity)
                );
            }
        }

        return ActionResult.Fail();
    }
}
