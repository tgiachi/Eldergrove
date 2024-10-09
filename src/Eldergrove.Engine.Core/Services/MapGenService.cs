using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class MapGenService : IMapGenService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, MapFabricObject> _mapFabrics = new();

    public MapGenService(ILogger<MapGenService> logger, IDataLoaderService dataLoaderService)
    {
        _logger = logger;

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
        _logger.LogDebug($"Map fabric {fabric.Name} added");
    }
}
