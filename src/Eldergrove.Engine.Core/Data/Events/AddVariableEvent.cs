using Eldergrove.Engine.Core.Attributes.Events;

namespace Eldergrove.Engine.Core.Data.Events;

[EventToDispatcher("add_variable")]
public record AddVariableEvent(string VariableName, object Value);
