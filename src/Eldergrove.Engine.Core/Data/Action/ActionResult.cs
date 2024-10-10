using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Action;

public class ActionResult
{
    public ActionResultType Result { get; set; }

    public ActionResult(ActionResultType result)
    {
        Result = result;
    }


    public static ActionResult Succeed() => new ActionResult(ActionResultType.Success);
    public static ActionResult Fail() => new ActionResult(ActionResultType.Failure);
    public static ActionResult WaitAction() => new ActionResult(ActionResultType.Wait);
    public static ActionResult RepeatAction() => new ActionResult(ActionResultType.Repeat);
}
