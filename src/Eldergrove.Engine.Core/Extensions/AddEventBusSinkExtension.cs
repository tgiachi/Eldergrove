using Eldergrove.Engine.Core.Sinks;
using Serilog;
using Serilog.Configuration;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddEventBusSinkExtension
{
    public static LoggerConfiguration EventBus(
        this LoggerSinkConfiguration loggerConfiguration,
        IFormatProvider formatProvider = null
    ) =>
        loggerConfiguration.Sink(new EventbusSink());
}
