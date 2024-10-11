using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.KeybindingActions.Base;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.KeybindingActions;

[KeybindingAction("map", "player_move_right")]
public class PlayerMoveRight(ISchedulerService schedulerService, INpcService npcService) : BasePlayerMovement(
    Direction.Right,
    schedulerService,
    npcService
)
{
};
