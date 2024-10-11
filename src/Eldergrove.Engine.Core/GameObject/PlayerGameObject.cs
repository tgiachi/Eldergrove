using Eldergrove.Engine.Core.Components;
using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class PlayerGameObject : NpcGameObject
{
    public PlayerGameObject(Point position, ColoredGlyph appearance) : base(position, appearance)
    {
    }


    public void ShowAllMap()
    {
        CurrentMap.PlayerFOV.Calculate(Position, 800);
        GoRogueComponents.GetFirstOrDefault<PlayerFOVController>().FOVRadius = 800;
        GoRogueComponents.GetFirstOrDefault<PlayerFOVController>().CalculateFOV();
    }
}
