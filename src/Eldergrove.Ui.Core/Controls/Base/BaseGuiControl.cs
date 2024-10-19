using Eldergrove.Engine.Core.State;
using SadConsole.UI;
using SadRogue.Primitives;

namespace Eldergrove.Ui.Core.Controls.Base;

using Console = SadConsole.Console;

public class BaseGuiControl : ControlsConsole
{
    public BaseGuiControl(int width, int height) : base(width, height)
    {
    }


    protected void ToCenter()
    {
        Position = new Point(
            (EldergroveState.ScreenSize.X) / 2 - Width / 2,
            (EldergroveState.ScreenSize.Y) / 2 - Height / 2
        );

        IsDirty = true;
    }
}
