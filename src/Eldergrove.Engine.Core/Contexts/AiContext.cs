using Eldergrove.Engine.Core.Actions.Npcs;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Maps;
using GoRogue.GameFramework;
using GoRogue.Pathing;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Contexts;

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

    public bool IsPlayerInRange(int radius)
    {
        return Radius.Circle.PositionsInRadius(Entity.Position, radius)
            .Any(point => point == Player.Position);
    }

    public List<IGameObject> GetEntitiesAtRange(int radius, int layer = 1)
    {
        var entities = new List<IGameObject>();

        Radius.Circle.PositionsInRadius(Entity.Position, radius)
            .ToList()
            .ForEach(
                point =>
                {
                    if (Map.GetObjectAt(point, (uint)layer) is IGameObject entity)
                    {
                        entities.Add(entity);
                    }
                }
            );

        return entities;
    }

    public List<Point> GetPathForPlayer()
    {
        var pathFinder = new AStar(Map.WalkabilityView, Map.DistanceMeasurement);

        var pathing = pathFinder.ShortestPath(Entity.Position, Player.Position);

        return pathing.Steps.ToList();
    }

    public void SetState(string key, object value)
    {
        _state[key] = value;
    }

    public object this[string key]
    {
        get => GetState(key);
        set => SetState(key, value);
    }


    public bool HasState(string key) => _state.ContainsKey(key);

    public object GetState(string key) => _state.ContainsKey(key) ? _state[key] : null;

    public Direction Left() => Direction.Left;

    public Direction Right() => Direction.Right;

    public Direction Up() => Direction.Up;

    public Direction Down() => Direction.Down;

    public Direction GoRandom()
    {
        var newDirection = Direction.GetDirection(Random.Shared.Next(0, 4), Random.Shared.Next(0, 4));

        return newDirection;
    }
}
