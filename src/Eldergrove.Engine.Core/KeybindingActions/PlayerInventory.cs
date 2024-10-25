using System.Security.Principal;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.KeybindingActions;

[KeybindingAction("map", "player_inventory")]
public class PlayerInventory : IKeybindingAction
{
    private readonly IMessageBusService _messageBusService;

    private readonly INpcService _npcService;

    public PlayerInventory(IMessageBusService messageBusService, INpcService npcService)
    {
        _messageBusService = messageBusService;
        _npcService = npcService;
    }


    public void Execute(ActionContext context)
    {
        _messageBusService.Publish(new GuiShowInventoryEvent(_npcService.Player));
    }
}
