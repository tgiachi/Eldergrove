using System.Globalization;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Components.Common;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Interfaces.Services;
using GoRogue.GameFramework;
using Microsoft.Extensions.Logging;
using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

[AutostartService(1)]
public class TileService : ITileService
{
    private readonly ILogger _logger;

    private readonly IColorService _colorService;


    private readonly Dictionary<string, TileEntry> _tiles = new();

    public TileService(ILogger<TileService> logger, IColorService colorService, IDataLoaderService dataLoaderService)
    {
        _logger = logger;
        _colorService = colorService;


        dataLoaderService.SubscribeData<TileSetObject>(
            async (tileSet) =>
            {
                foreach (var tile in tileSet.Tiles)
                {
                    AddTile(tile);
                }
            }
        );
    }

    public Task StartAsync() => Task.CompletedTask;

    public Task StopAsync() => Task.CompletedTask;

    public ColoredGlyph GetTile(string tileId)
    {
        if (_tiles.TryGetValue(tileId, out var tile))
        {
            return GetTile(tile);
        }

        _logger.LogWarning("Tile {TileId} not found", tileId);
        throw new KeyNotFoundException($"Tile {tileId} not found");
    }

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

        if (tileData.Symbol.StartsWith("!!"))
        {
            return new ColoredGlyph(foreground, background, int.Parse(tileData.Symbol[2].ToString()));
        }

        TileEntry tile = _tiles[tileData.Symbol];

        if (tile.Symbol.StartsWith("##"))
        {

            return new ColoredGlyph(foreground, background, tile.Symbol[2]);
        }

        if (tile.Symbol.StartsWith("!!"))
        {
            return new ColoredGlyph(foreground, background, int.Parse(tile.Symbol[2..]));
        }


        var colored = new ColoredGlyph(foreground, background, tile.Symbol[0]);


        return colored;
    }

    public (ColoredGlyph glyph, TileEntry tile) GetTileWithEntry(IJsonSymbolDataObject tileData) =>
        _tiles.TryGetValue(tileData.Symbol, out var tile) ? (GetTile(tileData), tile) : (GetTile(tileData), new TileEntry());

    //_logger.LogWarning("Tile {TileId} not found", tileData.Symbol);
    //throw new KeyNotFoundException($"Tile {tileData.Symbol} not found");
    public void AddTile(TileEntry tileEntry)
    {
        _logger.LogDebug("Adding tile {TileId}", tileEntry.Id);
        _tiles.Add(tileEntry.Id, tileEntry);
    }

    public void BuildTileAnimation<TGameObject>(TGameObject gameObject, TileEntry tileEntry) where TGameObject : IGameObject
    {
        if (tileEntry.Animation != null)
        {
            Color? startForeground = null;
            Color? endForeground = null;
            Color? startBackground = null;
            Color? endBackground = null;


            if (tileEntry.Animation.Starting.Foreground != null && tileEntry.Animation.Ending.Foreground != null)
            {
                startForeground = _colorService.GetColor(tileEntry.Animation.Starting.Foreground);
                endForeground = _colorService.GetColor(tileEntry.Animation.Ending.Foreground);
            }

            if (tileEntry.Animation.Starting.Background != null && tileEntry.Animation.Ending.Background != null)
            {
                startBackground = _colorService.GetColor(tileEntry.Animation.Starting.Background);
                endBackground = _colorService.GetColor(tileEntry.Animation.Ending.Background);
            }


            var animation = tileEntry.Animation;
            var startingSymbol = animation.Starting.Symbol;
            var endSymbol = animation.Ending.Symbol;

            var animationComponent = new TileAnimationComponent(
                startingSymbol,
                endSymbol,
                startForeground,
                endForeground,
                startBackground,
                endBackground
            );
            gameObject.GoRogueComponents.Add(animationComponent);
        }
    }


    private Color GetColor(string colorName)
    {
        if (colorName.StartsWith("#"))
        {
            var r = byte.Parse(colorName.Substring(1, 2), NumberStyles.HexNumber);
            var g = byte.Parse(colorName.Substring(3, 2), NumberStyles.HexNumber);
            var b = byte.Parse(colorName.Substring(5, 2), NumberStyles.HexNumber);
            var a = 255;
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
