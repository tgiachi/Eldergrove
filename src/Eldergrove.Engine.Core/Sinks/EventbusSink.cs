using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.State;
using Serilog.Core;
using Serilog.Events;

namespace Eldergrove.Engine.Core.Sinks;

public class EventbusSink : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
        if (EldergroveState.Engine != null)
        {
            var eventBus = EldergroveState.Engine.GetService<IMessageBusService>();

            eventBus.Publish(
                new LoggerEvent(
                    logEvent.Level,
                    logEvent.RenderMessage(),
                    logEvent.Properties.ContainsKey("Source") ? logEvent.Properties["Source"].ToString() : null,
                    logEvent.Exception?.ToString()
                )
            );
        }
    }
}
