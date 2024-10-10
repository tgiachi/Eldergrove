using Eldergrove.Engine.Core.Attributes.Events;

namespace Eldergrove.Engine.Core.Data.Events;

[EventToDispatcher("on_message_log")]
public record MessageLogEvent(string Message, string Background, string Foreground);
