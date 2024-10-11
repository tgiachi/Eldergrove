using System.Text;
using SadConsole.Input;

namespace Eldergrove.Engine.Core.Data.Keyboard;

public record KeybindingData(Keys Key, bool Ctrl, bool Shift, bool Alt)
{
    public override string ToString()
    {
        var sb = new StringBuilder();

        if (Ctrl)
        {
            sb.Append("CTRL+");
        }

        if (Shift)
        {
            sb.Append("SHIFT+");
        }

        if (Alt)
        {
            sb.Append("ALT+");
        }

        sb.Append(Key.ToString());

        return sb.ToString();
    }
}
