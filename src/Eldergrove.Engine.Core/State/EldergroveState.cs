using Eldergrove.Engine.Core.Interfaces.Manager;

namespace Eldergrove.Engine.Core.State;

public static class EldergroveState
{
    public static IEldergroveEngine? Engine { get; set; } = null!;
}
