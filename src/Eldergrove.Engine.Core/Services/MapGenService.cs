using System.Text.Json;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Game;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.Utils;
using GoRogue.MapGeneration;
using Microsoft.Extensions.Logging;
using NLua;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class MapGenService : IMapGenService
{
    private readonly ILogger _logger;

    public GameMap CurrentMap { get; private set; }

    private readonly Dictionary<string, MapFabricObject> _mapFabrics = new();

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    private readonly IScriptEngineService _scriptEngineService;

    private GameConfig _gameConfig;

    public MapGenService(
        ILogger<MapGenService> logger, IDataLoaderService dataLoaderService, IScriptEngineService scriptEngineService,
        JsonSerializerOptions jsonSerializerOptions
    )
    {
        _logger = logger;
        _scriptEngineService = scriptEngineService;
        _jsonSerializerOptions = jsonSerializerOptions;

        dataLoaderService.SubscribeData<MapFabricObject>(OnMapFabric);
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
            _logger.LogWarning($"Map fabric with id {fabric.Id} already exists");
            return;
        }

        _logger.LogDebug("Map fabric {Fabric} added", fabric.Id);
    }

    public async Task GenerateMapAsync()
    {
        if (!_scriptEngineService.ContextVariables.TryGetValue("game_config", out var gameConfig))
        {
            _logger.LogError("Game config not found");
            throw new Exception("Game config not found");
        }

        var json = JsonSerializer.Serialize(ScriptUtils.LuaTableToDictionary((LuaTable)gameConfig), _jsonSerializerOptions);

        _gameConfig = JsonSerializer.Deserialize<GameConfig>(json, _jsonSerializerOptions);


        // Generate a dungeon maze map
        var generator = new Generator(_gameConfig.MapWidth, _gameConfig.MapHeight)
            .ConfigAndGenerateSafe(
                gen =>
                {
                    gen.AddSteps(
                        DefaultAlgorithms.RectangleMapSteps()
                    );
                }
            );

        generator.Generate();

        CurrentMap = new GameMap(_gameConfig.MapWidth, _gameConfig.MapHeight, null);
    }
}
