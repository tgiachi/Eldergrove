using System.Text.Json;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.Game;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using GoRogue.MapGeneration;
using Microsoft.Extensions.Logging;

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

    private GameConfig _gameConfig;

    public MapGenService(
        ILogger<MapGenService> logger, IDataLoaderService dataLoaderService, IScriptEngineService scriptEngineService,
        IMessageBusService messageBusService
    )
    {
        _logger = logger;
        _scriptEngineService = scriptEngineService;
        _messageBusService = messageBusService;


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

    public async Task GenerateMapAsync()
    {
        _gameConfig = _scriptEngineService.GetContextVariable<GameConfig>("game_config");

        var generator = new Generator(_gameConfig.Map.Width, _gameConfig.Map.Height)
            .ConfigAndGenerateSafe(
                gen =>
                {
                    gen.AddSteps(
                        DefaultAlgorithms.RectangleMapSteps()
                    );
                }
            );

        generator.Generate();

        CurrentMap = new GameMap(_gameConfig.Map.Width, _gameConfig.Map.Height, null);

        _messageBusService.Publish(new MapGeneratedEvent(CurrentMap));
    }
}
