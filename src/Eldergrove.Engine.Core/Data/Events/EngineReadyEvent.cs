using Eldergrove.Engine.Core.Attributes.Events;

namespace Eldergrove.Engine.Core.Data.Events;

[EventToDispatcher("engine_ready")]
public record EngineReadyEvent;
