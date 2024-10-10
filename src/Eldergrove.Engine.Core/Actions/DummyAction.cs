using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Action;

namespace Eldergrove.Engine.Core.Actions;

[SchedulerAction("dummy")]
public class DummyAction : AbstractSchedulerAction
{
    private int _waitTurns = 10;

    public override async Task<ActionResult> ExecuteAsync()
    {
        _waitTurns--;
        return _waitTurns > 0 ? ActionResult.Succeed() : ActionResult.WaitAction();
    }
}
