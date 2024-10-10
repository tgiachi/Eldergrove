using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Maps;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Actions.Player;

[SchedulerAction("player_movement")]
public class PlayerMovementAction : AbstractSchedulerAction
{
    private readonly Direction _direction;
    private readonly PlayerGameObject _player;

    public PlayerMovementAction(Direction direction, PlayerGameObject player)
    {
        _direction = direction;

        _player = player;
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        var newPosition = _player.Position + _direction;
        if (_player.CurrentMap.GameObjectCanMove(_player, newPosition))
        {
            _player.Position = newPosition;


            return ActionResult.Succeed();
        }

        return ActionResult.Fail();
    }
}
