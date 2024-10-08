using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class ItemGameObject : RogueLikeCell
{
    public string ItemId { get; set; }


    public ItemGameObject(
        Point position, ColoredGlyph appearance, bool walkable = true, bool transparent = false
    ) : base(appearance, (int)MapLayerType.Items, walkable, transparent)
    {
    }


    public override string ToString() => $"Item: {ItemId}";
}
