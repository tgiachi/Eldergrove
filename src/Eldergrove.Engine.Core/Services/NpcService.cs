using Eldergrove.Engine.Core.Data.Json.Npcs;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

public class NpcService : INpcService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, NpcObject> _npcObjects = new();

    public NpcService(IDataLoaderService dataLoaderService, ILogger<NpcService> logger)
    {
        _logger = logger;


        dataLoaderService.SubscribeData<NpcObject>(OnNpcObject);
    }

    private NpcObject? GetById(string id) => _npcObjects.TryGetValue(id, out var npc) ? npc : null;

    private NpcObject? GetByCategory(string category)
    {
        return _npcObjects.Values.FirstOrDefault(npc => npc.Category == category);
    }

    private Task OnNpcObject(NpcObject arg)
    {
        AddNpc(arg);

        return Task.CompletedTask;
    }

    public NpcGameObject BuildGameObject(string id, Point position)
    {
        return null;
    }

    public void AddNpc(NpcObject npc)
    {
        _logger.LogDebug("Adding npc {NpcId}", npc.Id);
    }
}
