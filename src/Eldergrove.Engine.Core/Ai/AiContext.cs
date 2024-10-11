using Eldergrove.Engine.Core.Actions.Player;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Maps;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Ai;

public class AiContext
{
    public GameMap Map { get; set; }

    public PlayerGameObject Player { get; set; }

    public NpcGameObject Entity { get; set; }

    public override string ToString() => $"AiContext:";


    public List<ISchedulerAction> EmptyActionList() => new();


    public List<ISchedulerAction> DoMovement(Direction direction) => new()
    {
        new EntityMovementAction(direction, Entity)
    };

    public Direction GoLeft() => Direction.Left;

    public Direction GoRight() => Direction.Right;

    public Direction GoUp() => Direction.Up;

    public Direction GoDown() => Direction.Down;

    public Direction GoRandom() => Direction.GetDirection(new Point(Random.Shared.Next(-1, 1), Random.Shared.Next(-1, 1)));
}
