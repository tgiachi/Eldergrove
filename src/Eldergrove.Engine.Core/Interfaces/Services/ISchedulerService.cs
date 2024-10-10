using Eldergrove.Engine.Core.Interfaces.Actions;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface ISchedulerService
{
    int Turn { get; }

    void AddAction(ISchedulerAction action);
    
    Task TickAsync();
}
