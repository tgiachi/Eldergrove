using System.Globalization;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

public class TileService : ITileService
{
    private readonly ILogger _logger;

    private readonly IColorService _colorService;

    private readonly Dictionary<string, TileEntry> _tiles = new();

    public TileService(ILogger<TileService> logger, IColorService colorService)
    {
        _logger = logger;
        _colorService = colorService;
    }

    public Task StartAsync() => Task.CompletedTask;

    public Task StopAsync() => Task.CompletedTask;

    public ColoredGlyph GetTile(IJsonSymbolDataObject tileData)
    {
        Color foreground = Color.White;
        Color background = Color.Black;
        if (tileData.Background != null)
        {
            background = GetColor(tileData.Background);
        }

        if (tileData.Foreground != null)
        {
            foreground = GetColor(tileData.Foreground);
        }

        if (tileData.Symbol.StartsWith("##"))
        {
            return new ColoredGlyph(foreground, background, tileData.Symbol[2]);
        }

        return new ColoredGlyph(foreground, background, tileData.Symbol[0]);
    }

    public void AddTile(TileEntry tileEntry)
    {
        _logger.LogInformation("Adding tile {TileId}", tileEntry.Id);
        _tiles.Add(tileEntry.Id, tileEntry);
    }


    private Color GetColor(string colorName)
    {
        if (colorName.StartsWith("#"))
        {
            byte r = byte.Parse(colorName.Substring(1, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(colorName.Substring(3, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(colorName.Substring(5, 2), NumberStyles.HexNumber);
            byte a = 255;
            if (colorName.Length == 9)
            {
                a = byte.Parse(colorName.Substring(7, 2), NumberStyles.HexNumber);
            }

            return new Color(r, g, b, a);
        }
        else
        {
            return _colorService.GetColor(colorName);
        }
    }
}
