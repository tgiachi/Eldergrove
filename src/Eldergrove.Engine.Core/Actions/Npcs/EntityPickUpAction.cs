using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Components.Common;
using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.GameObject;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Actions.Npcs;

[SchedulerAction("entity_pickup")]
public class EntityPickUpAction : AbstractSchedulerAction
{
    private readonly NpcGameObject _entity;

    public EntityPickUpAction(NpcGameObject entity)
    {
        _entity = entity;
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        var rangePosition = Radius.Circle.PositionsInRadius(_entity.Position, 1);

        IGameObject gameObject = null;

        foreach (var position in rangePosition)
        {
            gameObject = _entity.CurrentMap.GetEntityAt<PropGameObject>(position);
            if (gameObject != null)
            {
                break;
            }
        }

        if (gameObject == null)
        {
            return ActionResult.Fail();
        }


        if (_entity is PlayerGameObject playerGameObject)
        {
            if (gameObject.GoRogueComponents.Contains<InventoryComponent>())
            {
                var inventoryComponent = gameObject.GoRogueComponents.GetFirstOrDefault<InventoryComponent>();

                SendEventMessage(new GuiPickUpRequestEvent(playerGameObject, inventoryComponent.Items));
                return ActionResult.Succeed();
            }

            return ActionResult.Fail();
        }


        return ActionResult.Fail();
    }
}
