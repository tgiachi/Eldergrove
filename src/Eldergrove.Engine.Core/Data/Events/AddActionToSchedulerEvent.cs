using Eldergrove.Engine.Core.Interfaces.Actions;

namespace Eldergrove.Engine.Core.Data.Events;

public record AddActionToSchedulerEvent(ISchedulerAction Action);
