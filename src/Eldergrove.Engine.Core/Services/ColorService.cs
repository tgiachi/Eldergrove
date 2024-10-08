using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Json.Colors;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

[AutostartService(1)]
public class ColorService : IColorService
{
    private readonly Dictionary<string, Color> _colors = new();

    private readonly ILogger _logger;

    public ColorService(IDataLoaderService dataLoaderService, ILogger<ColorService> logger)
    {
        _logger = logger;
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
        _logger.LogDebug("Adding color {ColorName} with values {R}, {G}, {B}, {A}", colorName, r, g, b, a);
        _colors.Add(colorName, Color.FromNonPremultiplied(r, g, b, a));
    }
}
