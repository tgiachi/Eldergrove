using Eldergrove.Engine.Core.Attributes.Events;

namespace Eldergrove.Engine.Core.Data.Events;

[EventToDispatcher("map_start_generate")]
public record MapStartGenerateEvent;
