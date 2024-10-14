using Eldergrove.Engine.Core.Actions.Npcs;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.KeybindingActions;


[KeybindingAction("map", "player_search")]
public class PlayerExecuteSearch : IKeybindingAction
{
    private readonly ISchedulerService _schedulerService;
    private readonly INpcService _npcService;

    public PlayerExecuteSearch(ISchedulerService schedulerService, INpcService npcService)
    {
        _schedulerService = schedulerService;
        _npcService = npcService;
    }

    public void Execute(ActionContext context)
    {
        _schedulerService.AddAction(new SearchForAction(_npcService.Player));
        _schedulerService.TickAsync();
    }
}
