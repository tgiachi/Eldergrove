namespace Eldergrove.Engine.Core.Data.Game;

public class SchedulerConfig
{
    public bool IsTurnBased { get; set; } = true;

    public int TickDelay { get; set; } = 200;
}
