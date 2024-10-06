using Eldergrove.Engine.Core.Interfaces.Services;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

public class ColorService : IColorService
{
    private readonly Dictionary<string, Color> _colors = new();

    public Color GetColor(string colorName) => _colors[colorName];

    public void AddColor(string colorName, Color color)
    {
        _colors.Add(colorName, color);
    }

    public void AddColor(string colorName, byte r, byte g, byte b, byte a = 255)
    {
        _colors.Add(colorName, Color.FromNonPremultiplied(r, g, b, a));
    }
}
