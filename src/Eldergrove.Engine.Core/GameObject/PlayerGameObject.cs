using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;


public class PlayerGameObject : NpcGameObject
{
    public PlayerGameObject(Point position, ColoredGlyph appearance) : base(position, appearance)
    {
    }
}
