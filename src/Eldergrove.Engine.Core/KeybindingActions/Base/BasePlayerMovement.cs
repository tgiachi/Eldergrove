using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Interfaces.Actions;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.KeybindingActions.Base;

public abstract class BasePlayerMovement : IKeybindingAction
{
    private Direction _direction;

    protected BasePlayerMovement(Direction direction)
    {
        _direction = direction;
    }

    public void Execute(ActionContext context)
    {
    }
}
