using Eldergrove.Engine.Core.State;
using SadConsole;
using SadConsole.Input;
using SadConsole.UI;
using SadRogue.Primitives;

namespace Eldergrove.Ui.Core.Controls.Base;

using Console = SadConsole.Console;

public class BaseGuiControl : ControlsConsole
{
    public bool EscToClose { get; set; } = true;

    public BaseGuiControl(int width, int height) : base(width, height)
    {
        Font = EldergroveState.DefaultUiFont;
    }


    protected void ToCenter()
    {
        Position = new Point(
            (EldergroveState.ScreenSize.X) / 2 - Width / 2,
            (EldergroveState.ScreenSize.Y) / 2 - Height / 2
        );

        IsDirty = true;
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        if (keyboard.IsKeyPressed(Keys.Escape) && EscToClose)
        {
            IsEnabled = false;
            IsVisible = false;
            UseKeyboard = false;
            UseMouse = false;
            Parent.Children.Remove(this);
            return true;
        }

        return base.ProcessKeyboard(keyboard);
    }
}
