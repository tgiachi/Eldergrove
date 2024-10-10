using Eldergrove.Engine.Core.GameObject;
using SadRogue.Integration.FieldOfView.Memory;

namespace Eldergrove.Engine.Core.Components;

public class TerrainFOVVisibilityHandler : MemoryFieldOfViewHandlerBase
{
    protected override void ApplyMemoryAppearance(MemoryAwareRogueLikeCell terrain)
    {
        terrain.LastSeenAppearance.CopyAppearanceFrom(((TerrainGameObject)terrain).DarkAppearance);
    }
}
