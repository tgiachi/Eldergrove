using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Interfaces.Components;
using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class PropGameObject : RogueLikeEntity, INamedComponent
{
    public bool CanDestroy { get; set; }

    public string Name { get; set; }
    public bool IsContainer => GoRogueComponents.Contains<ItemsContainerComponent>();
    public ItemsContainerComponent ItemsContainer => GoRogueComponents.GetFirst<ItemsContainerComponent>();
    public bool IsDoor => GoRogueComponents.Contains<DoorComponent>();

    public DoorComponent Door => GoRogueComponents.GetFirst<DoorComponent>();

    public PropGameObject(
        Point position, ColoredGlyph appearance, bool walkable = true, bool transparent = true
    ) : base(appearance, walkable, transparent, (int)MapLayerType.Props)
    {
        Position = position;


    }

    public override string ToString() => $"ID: {ID} Prop: {Name}";
}
