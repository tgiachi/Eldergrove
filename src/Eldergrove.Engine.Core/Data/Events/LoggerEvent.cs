using Serilog.Events;

namespace Eldergrove.Engine.Core.Data.Events;

public record LoggerEvent(LogEventLevel Level, string Message, string? Source = null, string? Exception = null);
