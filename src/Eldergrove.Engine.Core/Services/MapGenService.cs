using System.Diagnostics;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.Game;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
using GoRogue.MapGeneration;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives.GridViews;

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

    private readonly ITileService _tileService;

    private GameConfig _gameConfig;

    public MapGenService(
        ILogger<MapGenService> logger, IDataLoaderService dataLoaderService, IScriptEngineService scriptEngineService,
        IMessageBusService messageBusService, ITileService tileService
    )
    {
        _logger = logger;
        _scriptEngineService = scriptEngineService;
        _messageBusService = messageBusService;
        _tileService = tileService;


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

        //TODO: Add TerrainFOVVisibilityHandler


        CurrentMap = new GameMap(_gameConfig.Map.Width, _gameConfig.Map.Height, null);

        var generatedMap = generator.Context.GetFirst<ISettableGridView<bool>>("WallFloor");

        CurrentMap.ApplyTerrainOverlay(
            generatedMap,
            (pos, val) =>
                val
                    ? new TerrainGameObject(pos, floorGlyph, floorTile.Id)
                    : new TerrainGameObject(pos, wallGlyph, wallTile.Id, false)
        );


        foreach (var fabric in mapGenerator.Fabrics)
        {
            foreach (var _ in Enumerable.Range(0, fabric.GetRandomValue()))
            {
                var fabricObject = GetFabric(fabric.Id);

                GenerateFabric(fabricObject, CurrentMap);
            }

        }

        _messageBusService.Publish(new MapGeneratedEvent(CurrentMap));

        stopWatch.Stop();
        _logger.LogDebug("Map generated in {Elapsed}ms", stopWatch.ElapsedMilliseconds);

        return CurrentMap;
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



        return fabric;
    }


    private void GenerateFabric(MapFabricObject fabric, GameMap map)
    {

        _logger.LogDebug("Finding free area for fabric {Fabric} area: {Area}", fabric.Id, fabric.Area);

        var freeArea = map.FindFreeArea(fabric.Width, fabric.Height);




    }
}
