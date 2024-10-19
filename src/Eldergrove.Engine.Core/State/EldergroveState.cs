using Eldergrove.Engine.Core.Interfaces.Manager;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.State;

public static class EldergroveState
{
    public static IEldergroveEngine? Engine { get; set; } = null!;


    public static Point ScreenSize { get; set; }
}
