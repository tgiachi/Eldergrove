using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class PropGameObject : RogueLikeCell
{
    public bool CanDestroy { get; set; }

    public bool IsContainer { get; set; }

    public List<ItemGameObject> ContainerItems { get; set; } = new();

    public PropGameObject(
        Point position, ColoredGlyph appearance, bool walkable = true, bool transparent = true
    ) : base(appearance, (int)MapLayerType.Props, walkable, transparent)
    {
    }
}
