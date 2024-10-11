using Eldergrove.Engine.Core.Data.Keyboard;
using SadConsole.Input;

namespace Eldergrove.Engine.Core.Extensions;

public static class KeyboardExtension
{
    public static KeybindingData ToKeybindingData(this Keyboard keyboard)
    {
        var isControlDown = keyboard.IsKeyPressed(Keys.LeftControl) || keyboard.IsKeyPressed(Keys.RightControl);
        var isShiftDown = keyboard.IsKeyPressed(Keys.LeftShift) || keyboard.IsKeyPressed(Keys.RightShift);
        var isAltDown = keyboard.IsKeyPressed(Keys.LeftAlt) || keyboard.IsKeyPressed(Keys.RightAlt);

        var key = keyboard.KeysPressed.Count > 0 ? keyboard.KeysPressed[0].Key : Keys.None;

        return new KeybindingData(key, isControlDown, isShiftDown, isAltDown);
    }
}
