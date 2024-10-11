using Eldergrove.Engine.Core.Data.Keyboard;
using SadConsole.Input;

namespace Eldergrove.Engine.Core.Utils;

public static class KeybindingParser
{
    public static KeybindingData Parse(string keybinding)
    {
        keybinding = keybinding.ToUpperInvariant();

        var parts = keybinding.Split(new[] { "+", " " }, StringSplitOptions.RemoveEmptyEntries);

        bool ctrl = parts.Contains("CTRL", StringComparer.OrdinalIgnoreCase);
        bool alt = parts.Contains("ALT", StringComparer.OrdinalIgnoreCase);
        bool shift = parts.Contains("SHIFT", StringComparer.OrdinalIgnoreCase);


        var keyString = parts.LastOrDefault(part => part != "CTRL" && part != "ALT" && part != "SHIFT");
        if (keyString == null)
        {
            throw new ArgumentException("Invalid keybinding format");
        }


        if (!Enum.TryParse<Keys>(keyString, true, out var key))
        {
            throw new ArgumentException($"Invalid key: {keyString}");
        }

        return new KeybindingData(key, ctrl, shift, alt);
    }
}
