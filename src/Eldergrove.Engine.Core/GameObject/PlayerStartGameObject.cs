using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class PlayerStartGameObject : PortalPropGameObject
{
    public PlayerStartGameObject(Point position) : base(
        position,
        new ColoredGlyph(Color.Transparent, Color.Transparent, 0)
    )
    {
        IsTransparent = true;
        IsWalkable = true;
    }
}
