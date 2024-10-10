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
    private readonly GameMap _gameMap;
    private readonly PlayerGameObject _player;

    public PlayerMovementAction(Direction direction, GameMap gameMap, PlayerGameObject player)
    {
        _direction = direction;
        _gameMap = gameMap;
        _player = player;
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        var newPosition = _player.Position + _direction;
        if (_gameMap.GameObjectCanMove(_player, newPosition))
        {
            _player.Position = newPosition;

            return ActionResult.Succeed();
        }

        return ActionResult.Fail();
    }
}
