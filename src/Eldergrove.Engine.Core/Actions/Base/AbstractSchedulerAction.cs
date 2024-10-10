using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.Interfaces.Actions;

namespace Eldergrove.Engine.Core.Actions.Base;

public class AbstractSchedulerAction : ISchedulerAction
{
    public virtual async Task<ActionResult> ExecuteAsync()
    {
        return ActionResult.Succeed();
    }
}
