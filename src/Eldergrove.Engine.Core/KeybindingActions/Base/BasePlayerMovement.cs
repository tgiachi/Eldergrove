using Eldergrove.Engine.Core.Actions.Player;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.KeybindingActions.Base;

public abstract class BasePlayerMovement : IKeybindingAction
{
    private readonly Direction _direction;
    private readonly ISchedulerService _schedulerService;
    private readonly INpcService _npcService;

    protected BasePlayerMovement(Direction direction, ISchedulerService schedulerService, INpcService npcService)
    {
        _direction = direction;
        _schedulerService = schedulerService;
        _npcService = npcService;
    }

    public void Execute(ActionContext context)
    {
        _schedulerService.AddAction(new EntityMovementAction(_direction, _npcService.Player));
        _schedulerService.TickAsync();
    }
}
