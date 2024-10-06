using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IColorService
{
    Color GetColor(string colorName);

    void AddColor(string colorName, Color color);

    void AddColor(string colorName, byte r, byte g, byte b, byte a = 255);
}
