using System.Diagnostics;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.Game;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using Microsoft.Extensions.Logging;
using SadConsole;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using SadRogue.Primitives.SpatialMaps;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class MapGenService : IMapGenService
{
    private readonly ILogger _logger;

    public GameMap CurrentMap { get; private set; }

    private readonly Dictionary<string, MapFabricObject> _mapFabrics = new();

    private readonly Dictionary<string, MapGeneratorObject> _mapGenerators = new();


    private readonly IMessageBusService _messageBusService;
    private readonly IScriptEngineService _scriptEngineService;
    private readonly ISchedulerService _schedulerService;

    private readonly IPropService _propService;
    private readonly INpcService _npcService;
    private readonly IItemService _itemService;
    private readonly ITileService _tileService;

    private GameConfig _gameConfig;

    public MapGenService(
        ILogger<MapGenService> logger, IDataLoaderService dataLoaderService, IScriptEngineService scriptEngineService,
        IMessageBusService messageBusService, ITileService tileService, IPropService propService, INpcService npcService,
        IItemService itemService, ISchedulerService schedulerService
    )
    {
        _logger = logger;
        _scriptEngineService = scriptEngineService;
        _messageBusService = messageBusService;
        _tileService = tileService;
        _propService = propService;
        _npcService = npcService;
        _itemService = itemService;
        _schedulerService = schedulerService;


        dataLoaderService.SubscribeData<MapFabricObject>(OnMapFabric);
        dataLoaderService.SubscribeData<MapGeneratorObject>(OnMapGenerator);
    }

    private Task OnMapGenerator(MapGeneratorObject arg)
    {
        if (!_mapGenerators.TryAdd(arg.Id, arg))
        {
            _logger.LogWarning("Map generator with id {generatorId} already exists", arg.Id);
            return Task.CompletedTask;
        }

        _logger.LogDebug("Map generator {Generator} added", arg.Id);
        return Task.CompletedTask;
    }

    private Task OnMapFabric(MapFabricObject arg)
    {
        AddFabric(arg);
        return Task.CompletedTask;
    }


    public void AddFabric(MapFabricObject fabric)
    {
        if (!_mapFabrics.TryAdd(fabric.Id, fabric))
        {
            _logger.LogWarning("Map fabric with id {fabricId} already exists", fabric.Id);
            return;
        }

        _logger.LogDebug("Map fabric {Fabric} added", fabric.Id);
    }

    public async Task<GameMap> GenerateMapAsync()
    {
        _gameConfig = _scriptEngineService.GetContextVariable<GameConfig>("game_config");

        _mapGenerators.TryGetValue(_gameConfig.Map.GeneratorId, out var mapGenerator);


        if (mapGenerator == null)
        {
            _logger.LogError("Map generator with id {GeneratorId} not found", _gameConfig.Map.GeneratorId);
            throw new InvalidOperationException("Map generator not found named " + _gameConfig.Map.GeneratorId);
        }

        var stopWatch = Stopwatch.StartNew();


        var generator = new Generator(_gameConfig.Map.Width, _gameConfig.Map.Height)
            .ConfigAndGenerateSafe(
                gen =>
                {
                    if (mapGenerator.GeneratorType == MapGeneratorType.Container)
                    {
                        _logger.LogDebug("Using container generator");
                        gen.AddSteps(
                            DefaultAlgorithms.RectangleMapSteps()
                        );
                    }
                }
            );

        generator.Generate();


        var (wallGlyph, wallTile) = _tileService.GetTileWithEntry(mapGenerator.Wall);
        var (floorGlyph, floorTile) = _tileService.GetTileWithEntry(mapGenerator.Floor);


        CurrentMap = new GameMap(_gameConfig.Map.Width, _gameConfig.Map.Height, null);

        CurrentMap.AllComponents.Add(new TerrainFOVVisibilityHandler());

        var generatedMap = generator.Context.GetFirst<ISettableGridView<bool>>("WallFloor");

        CurrentMap.ObjectAdded += OnEntityAdded;

        CurrentMap.ApplyTerrainOverlay(
            generatedMap,
            (pos, val) =>
                val
                    ? new TerrainGameObject(pos, floorGlyph, floorTile.Id)
                    : new TerrainGameObject(pos, wallGlyph, wallTile.Id, false)
        );


        foreach (var fabric in mapGenerator.Fabrics)
        {
            var fabricCount = fabric.GetRandomValue();

            _logger.LogDebug("Generating fabric {Fabric} {Count} times", fabric.Id, fabricCount);
            foreach (var _ in Enumerable.Range(0, fabricCount))
            {
                var fabricObject = GetFabric(fabric.Id);

                GenerateFabric(fabricObject, CurrentMap, (wallGlyph, wallTile), (floorGlyph, floorTile));
            }
        }

        _messageBusService.Publish(new MapGeneratedEvent(CurrentMap));

        stopWatch.Stop();
        _logger.LogDebug("Map generated in {Elapsed}ms", stopWatch.ElapsedMilliseconds);

        return CurrentMap;
    }

    private void OnEntityAdded(object? sender, ItemEventArgs<IGameObject> e)
    {
        if (e.Item is IActionableEntity actionableEntity)
        {
            _schedulerService.AddActionableEntity(actionableEntity);
        }
    }

    private MapFabricObject GetFabric(string idOrCategory)
    {
        MapFabricObject fabric = null;

        fabric = _mapFabrics.GetValueOrDefault(idOrCategory);

        if (fabric == null)
        {
            var category = _mapFabrics.Values.Where(x => x.Category == idOrCategory).ToList();

            if (category.Any())
            {
                fabric = category.RandomElement();
            }
        }


        if (fabric == null)
        {
            _logger.LogError("Fabric with idOrCategory {FabricId} not found", idOrCategory);
            throw new InvalidOperationException("Fabric not found named " + idOrCategory);
        }

        if (fabric.CanRotate)
        {
            fabric.Fabric = fabric.Fabric.RandomRotateFabric();
        }


        return fabric;
    }


    private void GenerateFabric(
        MapFabricObject fabric, GameMap map, (ColoredGlyph, TileEntry) wall, (ColoredGlyph, TileEntry) floor
    )
    {
        _logger.LogTrace(
            "Finding free area for fabric {Fabric} area: {Area} (W: {W} H: {H})",
            fabric.Id,
            fabric.Area,
            fabric.Width,
            fabric.Height
        );

        var freeArea = map.FindFreeArea(fabric.Width, fabric.Height);

        if (freeArea == null)
        {
            _logger.LogWarning("No free area found for fabric {Fabric}", fabric.Id);
            return;
        }

        _logger.LogDebug(
            "Free area found for fabric {Fabric} in {Point} - (Area: {Area} - W: {W} H: {H})",
            fabric.Id,
            freeArea[0],
            fabric.Area,
            fabric.Width,
            fabric.Height
        );

        var fabricArray = fabric.ToArray;

        for (int x = 0; x < fabric.Width; x++)
        {
            for (int y = 0; y < fabric.Height; y++)
            {
                var realX = freeArea[0].X + x;
                var realY = freeArea[0].Y + y;

                var tile = fabricArray[y][x].ToString();

                var isWall = tile == fabric.Wall.Symbol;

                var (glyph, tileEntry) = isWall ? wall : floor;

                var terrain = new TerrainGameObject(new Point(realX, realY), glyph, tileEntry.Id, !isWall, !isWall);

                map.SetTerrain(terrain);

                foreach (var layer in fabric.Layers.Keys)
                {
                    if (fabric.Layers[layer].TryGetValue(tile, out var id))
                    {
                        var gameObject = CreateGameObject(layer, id, new Point(realX, realY));
                        if (gameObject != null)
                        {
                            _logger.LogDebug("Adding {GameObject} to map", gameObject);
                            map.AddEntity(gameObject);
                        }
                    }
                }
            }
        }
    }

    private IGameObject? CreateGameObject(MapLayerType layer, string id, Point position)
    {
        if (layer == MapLayerType.Props)
        {
            return _propService.BuildGameObject(id, position);
        }

        if (layer == MapLayerType.Npc)
        {
            return _npcService.BuildGameObject(id, position);
        }

        if (layer == MapLayerType.Items)
        {
            return _itemService.BuildGameObject(id, position);
        }

        throw new InvalidOperationException("Unknown layer type " + layer);
    }
}
