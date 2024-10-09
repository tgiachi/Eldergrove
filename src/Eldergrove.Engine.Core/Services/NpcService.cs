using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Json.Npcs;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class NpcService : INpcService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, NpcObject> _npcObjects = new();

    private readonly ITileService _tileService;

    private readonly INameGeneratorService _nameGeneratorService;

    public NpcService(IDataLoaderService dataLoaderService, ILogger<NpcService> logger, ITileService tileService, INameGeneratorService nameGeneratorService)
    {
        _logger = logger;
        _tileService = tileService;
        _nameGeneratorService = nameGeneratorService;


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

    public NpcGameObject BuildGameObject(string idOrCategory, Point position)
    {
        var npc = GetById(idOrCategory) ?? GetByCategory(idOrCategory);

        NpcGameObject gameObject = null!;

        if (npc == null)
        {
            throw new InvalidOperationException($"No npc found with id or category {idOrCategory}");
        }

        var tile = _tileService.GetTile(npc);

        var name = _nameGeneratorService.GenerateName(npc.Name);

        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidOperationException($"No name found for npc {npc.Name}");
        }

        gameObject = new NpcGameObject(position, tile)
        {
            Name = name
        };

        return gameObject;
    }

    public void AddNpc(NpcObject npc)
    {
        _logger.LogDebug("Adding npc {NpcId}", npc.Id);
    }
}
