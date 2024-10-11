using Eldergrove.Engine.Core.Actions.Player;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Maps;
using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Ai;

public class AiContext
{
    public GameMap Map { get; set; }


    private readonly Dictionary<string, object> _state = new();

    public PlayerGameObject Player { get; set; }

    public NpcGameObject Entity { get; set; }

    public override string ToString() => $"AiContext:";


    public List<ISchedulerAction> EmptyActionList() => new();

    public List<ISchedulerAction> DoMovement(Direction direction) => new()
    {
        new EntityMovementAction(direction, Entity)
    };

    public List<IGameObject> GetEntitiesAtRange(int radius)
    {
        var entities = new List<IGameObject>();


        return entities;
    }

    public void SetState(string key, object value)
    {
        _state[key] = value;
    }

    public bool HasState(string key) => _state.ContainsKey(key);

    public object GetState(string key) => _state.ContainsKey(key) ? _state[key] : null;

    public Direction GoLeft() => Direction.Left;

    public Direction GoRight() => Direction.Right;

    public Direction GoUp() => Direction.Up;

    public Direction GoDown() => Direction.Down;

    public Direction GoRandom() => Direction.GetDirection(new Point(Random.Shared.Next(-1, 1), Random.Shared.Next(-1, 1)));
}
