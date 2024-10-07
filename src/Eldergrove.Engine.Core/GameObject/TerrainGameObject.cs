using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
using SadConsole;
using SadRogue.Integration.FieldOfView.Memory;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class TerrainGameObject : MemoryAwareRogueLikeCell
{
    public ColoredGlyph DarkAppearance { get; }

    public TerrainGameObject(
        Point position, ColoredGlyph appearance, bool walkable = true, bool transparent = true
    ) : base(position, appearance, (int)MapLayerType.Terrain, walkable, transparent)
    {
        DarkAppearance = ColorUtils.Darken(appearance, 0.5f);
    }
}
