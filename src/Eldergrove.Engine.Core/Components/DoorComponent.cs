using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Components;
using SadConsole;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class DoorComponent : RogueLikeComponentBase<PropGameObject>, IActionableComponent
{
    private readonly ColoredGlyph _openDoor;
    private readonly ColoredGlyph _closedDoor;

    public bool IsOpen { get; private set; }

    public DoorComponent(ColoredGlyph openDoor, ColoredGlyph coloredGlyph, bool isOpen = false) : base(
        false,
        false,
        false,
        false
    )
    {
        _openDoor = openDoor;
        _closedDoor = coloredGlyph;
        IsOpen = isOpen;
    }

    public void Action()
    {
        Parent.Appearance.CopyAppearanceFrom(IsOpen ? _closedDoor : _openDoor);
    }
}
