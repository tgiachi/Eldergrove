using Eldergrove.Engine.Core.Interfaces.Manager;
using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.State;

public static class EldergroveState
{
    public static IEldergroveEngine? Engine { get; set; } = null!;


    public static IFont DefaultUiFont { get; set; } = null!;

    public static Point ScreenSize { get; set; }
}
