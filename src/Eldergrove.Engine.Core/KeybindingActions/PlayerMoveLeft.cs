using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.KeybindingActions.Base;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.KeybindingActions;

[KeybindingAction("map", "player_move_left")]
public class PlayerMoveLeft(ISchedulerService schedulerService, INpcService npcService) : BasePlayerMovement(
    Direction.Left,
    schedulerService,
    npcService
)
{
};
