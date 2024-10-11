using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.KeybindingActions.Base;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.KeybindingActions;

[KeybindingAction("map", "player_move_down")]
public class PlayerMoveDown(ISchedulerService schedulerService, INpcService npcService) : BasePlayerMovement(
    Direction.Down,
    schedulerService,
    npcService
)
{
};
