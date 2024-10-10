using Eldergrove.Engine.Core.Data.Action;

namespace Eldergrove.Engine.Core.Interfaces.Actions;

public interface ISchedulerAction
{
    Task<ActionResult> ExecuteAsync();
}
