using Eldergrove.Engine.Core.Attributes.Events;
using Eldergrove.Engine.Core.Data.MessageLog;

namespace Eldergrove.Engine.Core.Data.Events;

[EventToDispatcher("on_message_log")]
public record MessageLogEvent(MessageLogData Data);
