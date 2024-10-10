using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;


namespace Eldergrove.Engine.Core.Actions.Tests;

[SchedulerAction("dummy")]
public class DummyAction : TimedAction
{
    public DummyAction(int waitTurns = 10) : base(waitTurns)
    {
    }
}
