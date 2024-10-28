using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.State;

namespace Eldergrove.Engine.Core.Actions.Base;

public class AbstractSchedulerAction : ISchedulerAction
{
    public virtual async Task<ActionResult> ExecuteAsync() => ActionResult.Succeed();

    protected void SendEventMessage<TMessage>(TMessage message) where TMessage : class
    {
        EldergroveState.Engine.GetService<IMessageBusService>().Publish(message);
    }
}
