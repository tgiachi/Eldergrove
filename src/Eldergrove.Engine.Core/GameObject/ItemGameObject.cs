using Eldergrove.Engine.Core.Interfaces.Components;
using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class ItemGameObject : RogueLikeEntity, INamedComponent
{
    public string ItemId { get; set; }
    public string Name { get; set; }

    public ItemGameObject(
        Point position, ColoredGlyph appearance, bool walkable = true, bool transparent = false
    ) : base(appearance, walkable, transparent, (int)MapLayerType.Items)
    {
        Position = position;
    }

    public override string ToString() => $"Item: {ItemId}";

}
