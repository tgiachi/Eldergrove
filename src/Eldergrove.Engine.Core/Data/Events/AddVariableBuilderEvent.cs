using Eldergrove.Engine.Core.Attributes.Events;

namespace Eldergrove.Engine.Core.Data.Events;

[EventToDispatcher("add_variable_builder")]
public record AddVariableBuilderEvent(string VariableName, Func<object> Builder);
