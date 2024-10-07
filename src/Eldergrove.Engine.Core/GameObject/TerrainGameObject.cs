using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration.FieldOfView.Memory;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class TerrainGameObject : MemoryAwareRogueLikeCell
{
    public TerrainGameObject(
        Point position, ColoredGlyph appearance, int layer, bool walkable = true, bool transparent = true
    ) : base(position, appearance, (int)MapLayerType.Terrain, walkable, transparent)
    {
    }
}
