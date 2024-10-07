using Eldergrove.Engine.Core.Types;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class PropGameObject : GoRogue.GameFramework.GameObject
{
    public PropGameObject(
        Point position, bool isWalkable = true, bool isTransparent = true
    ) : base(position, (int)MapLayerType.Object, isWalkable, isTransparent)
    {
    }
}
