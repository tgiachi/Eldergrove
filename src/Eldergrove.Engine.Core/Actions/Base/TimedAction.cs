using Eldergrove.Engine.Core.Data.Action;

namespace Eldergrove.Engine.Core.Actions.Base;

public abstract class TimedAction : AbstractSchedulerAction
{
    protected int WaitTurns { get; set; }

    protected TimedAction(int waitTurns)
    {
        WaitTurns = waitTurns;
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        WaitTurns--;
        //return WaitTurns > 0 ? ActionResult.WaitAction() : ActionResult.Succeed();

        if (WaitTurns > 0)
        {
            OnTick();
            return ActionResult.WaitAction();
        }

        OnComplete();

        return ActionResult.Succeed();
    }

    protected virtual void OnComplete()
    {
    }

    protected virtual void OnTick()
    {
    }
}
