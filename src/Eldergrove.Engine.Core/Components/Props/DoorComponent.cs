using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.MessageLog;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Components;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.State;
using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components.Props;

public class DoorComponent : RogueLikeComponentBase<PropGameObject>, IActionableComponent
{
    private readonly ColoredGlyph _openDoor;
    private readonly ColoredGlyph _closedDoor;

    public bool IsOpen { get; private set; }

    public DoorComponent(ColoredGlyph openDoor, ColoredGlyph closedDoor, bool isOpen = false) : base(
        false,
        false,
        false,
        false
    )
    {
        _openDoor = openDoor;
        _closedDoor = closedDoor;
        IsOpen = isOpen;

        Added += OnAdded;
    }

    private void OnAdded(object? sender, EventArgs e)
    {
        if (IsOpen)
        {
            Parent.IsTransparent = true;
            Parent.IsWalkable = true;
        }
        else
        {
            Parent.IsTransparent = false;
            Parent.IsWalkable = false;
        }
    }

    public void Action()
    {
        IsOpen = !IsOpen;
        Parent.IsTransparent = IsOpen;
        Parent.IsWalkable = IsOpen;

        var currentAppearance = IsOpen ? _openDoor : _closedDoor;

        Parent.AppearanceSingle.Appearance.Glyph = currentAppearance.Glyph;
        Parent.AppearanceSingle.Appearance.Foreground = currentAppearance.Foreground;
        Parent.AppearanceSingle.Appearance.Background = currentAppearance.Background;


        var message = IsOpen ? "The door opens." : "The door closes.";

        EldergroveState.Engine.GetService<IMessageBusService>()
            .Publish(new MessageLogEvent(new MessageLogData(message, MessageLogType.Info)));
    }
}
