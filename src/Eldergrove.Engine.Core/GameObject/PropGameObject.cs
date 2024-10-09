using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Interfaces.Components;
using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class PropGameObject : RogueLikeCell, INamedComponent
{
    public bool CanDestroy { get; set; }

    public string Name { get; set; }
    public bool IsContainer => GoRogueComponents.Contains<ItemsContainerComponent>();
    public ItemsContainerComponent ItemsContainer => GoRogueComponents.GetFirst<ItemsContainerComponent>();
    public bool IsDoor => GoRogueComponents.Contains<DoorComponent>();

    public PropGameObject(
        Point position, ColoredGlyph appearance, bool walkable = true, bool transparent = true
    ) : base(appearance, (int)MapLayerType.Props, walkable, transparent)
    {
    }
}
