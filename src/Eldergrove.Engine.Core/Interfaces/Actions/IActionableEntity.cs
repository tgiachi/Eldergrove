namespace Eldergrove.Engine.Core.Interfaces.Actions;

public interface IActionableEntity
{
    IEnumerable<ISchedulerAction> TakeTurn();
}
