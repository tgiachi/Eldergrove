using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Action;

public class ActionResult
{
    public ActionResultType Result { get; set; }

    public ISchedulerAction? AlternateAction { get; set; }

    public ActionResult(ActionResultType result, ISchedulerAction action = null)
    {
        Result = result;
        AlternateAction = action;
    }


    public static ActionResult Succeed(ISchedulerAction action = null) => new ActionResult(ActionResultType.Success, action);
    public static ActionResult Fail(ISchedulerAction action = null) => new ActionResult(ActionResultType.Failure, action);
    public static ActionResult WaitAction() => new ActionResult(ActionResultType.Wait);
    public static ActionResult RepeatAction() => new ActionResult(ActionResultType.Repeat);
}
