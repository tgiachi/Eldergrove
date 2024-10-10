using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Action;

namespace Eldergrove.Engine.Core.Actions.Tests;

[SchedulerAction("dummy")]
public class DummyAction : AbstractSchedulerAction
{
    private int _waitTurns;

    public DummyAction(int waitTurns = 10)
    {
        _waitTurns = waitTurns;
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        _waitTurns--;
        return _waitTurns > 0 ? ActionResult.WaitAction() : ActionResult.Succeed();
    }
}
