using System.Diagnostics;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Components.Npcs;
using Eldergrove.Engine.Core.Components.Terrain;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.Game;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Map;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.Types;
using GoRogue.GameFramework;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;
using SadRogue.Primitives.SpatialMaps;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class MapGenService : IMapGenService
{
    private readonly ILogger _logger;

    public GameMap CurrentMap { get; private set; }

    private readonly Dictionary<string, MapFabricObject> _mapFabrics = new();

    private readonly Dictionary<string, MapGeneratorObject> _mapGenerators = new();

    private readonly Dictionary<string, GameMap> _maps = new();

    private readonly Dictionary<MapLayerType, List<IGameObject>> _layeredObjects = new();

    private readonly List<MapGeneratorData> _generators;

    private readonly IServiceProvider _serviceProvider;

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
        IItemService itemService, ISchedulerService schedulerService, List<MapGeneratorData> generators,
        IServiceProvider serviceProvider
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
        _generators = generators;
        _serviceProvider = serviceProvider;


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


    public List<TGameObject> GetEntities<TGameObject>(MapLayerType layerType) where TGameObject : IGameObject =>
        _layeredObjects.GetValueOrDefault(layerType)?.Cast<TGameObject>().ToList() ?? [];

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

        _messageBusService.Publish(new MapStartGenerateEvent());
        var stopWatch = Stopwatch.StartNew();


        GameMap map;

        var mapGenData = _generators.FirstOrDefault(s => s.Type == mapGenerator.GeneratorType);

        if (mapGenData == null)
        {
            _logger.LogError("Map generator with type {GeneratorType} not found", mapGenerator.GeneratorType);
            throw new InvalidOperationException("Map generator not found named " + mapGenerator.GeneratorType);
        }

        var mapGenType = _serviceProvider.GetService(mapGenData.ImplementationType) as IMapGenerator;

        map = await mapGenType.GenerateMapAsync(
            mapGenerator,
            new Point(_gameConfig.Map.Width, _gameConfig.Map.Height),
            mapGenData.Type
        );

        map.AllComponents.Add(new TerrainFOVVisibilityHandler());

        map.ObjectAdded += OnEntityAdded;
        map.ObjectRemoved += OnEntityRemoved;

        await mapGenType.PopulateMapAsync(map);

        if (CurrentMap == null)
        {
            CurrentMap = map;
        }


        _messageBusService.Publish(new MapGeneratedEvent(map));


        stopWatch.Stop();
        _logger.LogDebug("Map generated in {Elapsed}ms", stopWatch.ElapsedMilliseconds);

        return map;
    }

    private void OnEntityRemoved(object? sender, ItemEventArgs<IGameObject> e)
    {
        if (e.Item is IActionableEntity actionableEntity)
        {
            _schedulerService.RemoveActionableEntity(actionableEntity);
        }
    }

    private void OnEntityAdded(object? sender, ItemEventArgs<IGameObject> e)
    {
        if (e.Item is IActionableEntity actionableEntity)
        {
            _schedulerService.AddActionableEntity(actionableEntity);
        }

        if (e.Item.Layer > 1)
        {
            _logger.LogDebug("Entity added {Entity} in position: {Pos}", e.Item.GetType(), e.Position);

            if (!_layeredObjects.TryGetValue((MapLayerType)e.Item.Layer, out var layer))
            {
                _layeredObjects.Add((MapLayerType)e.Item.Layer, []);
            }

            _layeredObjects[(MapLayerType)e.Item.Layer].Add(e.Item);
        }

        if (e.Item is PlayerGameObject playerGameObject)
        {
            playerGameObject.AllComponents.GetFirstOrDefault<PlayerFOVController>().CalculateFOV();
        }
    }

    public MapFabricObject GetFabric(string idOrCategory)
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

        if (fabric.CanRotate && fabric.Area >= 16)
        {
            fabric.Fabric = fabric.Fabric.RandomRotateFabric();
        }


        return fabric;
    }

    public GeneratedFabricLayersData GenerateFabricAsync(
        MapFabricObject fabric, GlyphTileEntry wall, GlyphTileEntry floor, GameMap map,
        Point? startingPoint = null
    )
    {
        var result = new GeneratedFabricLayersData(fabric);

        var points = new List<Point>();

        if (startingPoint == null)
        {
            points.AddRange(map.FindFreeArea(fabric.Width, fabric.Height));
        }
        else
        {
            points.AddRange(MapExtension.PreAllocatePoints(fabric.Width, fabric.Height, startingPoint.Value));
        }

        var fabricArray = fabric.ToArray;

        for (int x = 0; x < fabric.Width; x++)
        {
            for (int y = 0; y < fabric.Height; y++)
            {
                var realX = points[0].X + x;
                var realY = points[0].Y + y;

                var tile = fabricArray[y][x].ToString();

                var isWall = tile == fabric.Wall.Symbol;

                var (glyph, tileEntry) = isWall ? wall : floor;

                var terrain = new TerrainGameObject(new Point(realX, realY), glyph, tileEntry.Id, !isWall, !isWall);

                result.AddLayer(MapLayerType.Terrain, terrain);


                foreach (var layer in fabric.Layers.Keys)
                {
                    if (fabric.Layers[layer].TryGetValue(tile, out var id))
                    {
                        var gameObject = CreateGameObject(layer, id, new Point(realX, realY));
                        if (gameObject != null)
                        {
                            _logger.LogDebug("Creating {GameObject}", gameObject);
                            result.AddLayer(layer, gameObject);
                        }
                    }
                }
            }
        }

        return result;
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

    public MapFabricObject BuildGameObject(string idOrCategory, Point position) => GetFabric(idOrCategory);
}
