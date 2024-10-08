using Eldergrove.Engine.Core.Types;
using GoRogue.Components;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class PropGameObject : RogueLikeCell
{

    public bool CanDestroy { get; set; }

    public PropGameObject(
        Point position, ColoredGlyph appearance, bool walkable = true, bool transparent = true
    ) : base(appearance, (int)MapLayerType.Object, walkable, transparent)
    {
    }
}
