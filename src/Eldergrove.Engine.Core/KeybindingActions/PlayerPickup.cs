using Eldergrove.Engine.Core.Actions.Npcs;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.KeybindingActions;

[KeybindingAction("map", "player_pickup")]
public class PlayerPickup : IKeybindingAction
{
    private readonly ISchedulerService _schedulerService;

    private readonly ILogger _logger;


    public PlayerPickup(ILogger<PlayerPickup> logger, ISchedulerService schedulerService)
    {
        _schedulerService = schedulerService;
        _logger = logger;
    }


    public void Execute(ActionContext context)
    {
        _logger.LogDebug("Try to pick up items on position {Position}", context.Engine.NpcService.Player.Position);
        _schedulerService.AddAction(new EntityPickUpAction(context.Engine.NpcService.Player));
        _schedulerService.TickAsync();
    }
}
