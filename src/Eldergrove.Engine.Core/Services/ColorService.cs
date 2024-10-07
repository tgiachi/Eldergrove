using Eldergrove.Engine.Core.Data.Json.Colors;
using Eldergrove.Engine.Core.Interfaces.Services;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

public class ColorService : IColorService
{
    private readonly Dictionary<string, Color> _colors = new();


    public ColorService(IDataLoaderService dataLoaderService)
    {
        dataLoaderService.SubscribeData<ColorObject>(
            o =>
            {
                if (o.Value.Length == 3)
                {
                    AddColor(o.Id, (byte)o.Value[0], (byte)o.Value[1], (byte)o.Value[2]);
                }
                else if (o.Value.Length == 4)
                {
                    AddColor(o.Id, (byte)o.Value[0], (byte)o.Value[1], (byte)o.Value[2], (byte)o.Value[3]);
                }

                return Task.CompletedTask;
            }
        );
    }

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
