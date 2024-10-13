using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Json.Npcs;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Utils;
using Microsoft.Extensions.Logging;
using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class NpcService : INpcService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, NpcObject> _npcObjects = new();

    private readonly ITileService _tileService;

    private readonly INameGeneratorService _nameGeneratorService;

    private readonly IItemService _itemService;

    private readonly Dictionary<string, Func<AiContext, List<ISchedulerAction>>> _brains = new();

    public PlayerGameObject Player { get; set; }


    public NpcService(
        IDataLoaderService dataLoaderService, ILogger<NpcService> logger, ITileService tileService,
        INameGeneratorService nameGeneratorService, IItemService itemService
    )
    {
        _logger = logger;
        _tileService = tileService;
        _nameGeneratorService = nameGeneratorService;
        _itemService = itemService;


        dataLoaderService.SubscribeData<NpcObject>(OnNpcObject);
    }

    private NpcObject? GetById(string id) => _npcObjects.GetValueOrDefault(id);

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

        var name = npc.Name;

        if (name.StartsWith("@"))
        {
            name = _nameGeneratorService.GenerateName(name[1..]);
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidOperationException($"No name found for npc {npc.Name}");
        }

        gameObject = new NpcGameObject(position, tile)
        {
            Name = name
        };

        if (npc.Container != null)
        {
            gameObject.GoRogueComponents.Add(new ItemsContainerComponent(_itemService.GetRandomItems(npc.Container)));
        }


        var skills = new SkillsComponent
        {
            Health = npc.Skills.Health.GetRandomValue()
        };

        gameObject.GoRogueComponents.Add(skills);

        gameObject.GoRogueComponents.Add(
            new AiComponent(this)
            {
                BrainId = npc.BrainAi
            }
        );

        var (_, tileEntry) = _tileService.GetTileWithEntry(npc);

        _tileService.BuildTileAnimation(gameObject, tileEntry);

        return gameObject;
    }


    public void BuildPlayer(Point position)
    {
        ColoredGlyph playerEntry = null!;

        try
        {
            playerEntry = _tileService.GetTile("t_player");
        }
        catch (KeyNotFoundException)
        {
            playerEntry = new ColoredGlyph(Color.Azure, Color.Black, '@');
        }


        Player = new PlayerGameObject((1, 1), playerEntry);
        Player.GoRogueComponents.Add(new PlayerFOVController());
    }

    public PlayerGameObject GetPlayer() => Player;

    public void AddNpc(NpcObject npc)
    {
        _logger.LogDebug("Adding npc {NpcId}", npc.Id);

        _npcObjects.Add(npc.Id, npc);
    }

    public void AddBrain(string id, Func<AiContext, List<ISchedulerAction>> brain)
    {
        _logger.LogDebug("Adding brain {BrainId}", id);
        _brains.Add(id, brain);
    }

    public IEnumerable<ISchedulerAction> InvokeBrain(string id, AiContext context)
    {
        if (!_brains.TryGetValue(id, out var brain))
        {
            throw new InvalidOperationException($"No brain found with id {id}");
        }

        return brain.Invoke(context);
    }
}
