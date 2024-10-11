using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.KeybindingActions.Base;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.KeybindingActions;

[KeybindingAction("move_up")]
public class PlayerMoveUp : BasePlayerMovement
{
    public PlayerMoveUp() : base(Direction.Up)
    {
    }
}
