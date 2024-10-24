using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Json.Items;
using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Utils;
using Microsoft.Extensions.Logging;
using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

[AutostartService(1)]
public class ItemService : IItemService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, ItemObject> _items = new();

    private readonly ITileService _tileService;

    private readonly Dictionary<string, Action<ItemFeatureContext>> _itemFeatures = new();


    public ItemService(ILogger<ItemService> logger, IDataLoaderService dataLoaderService, ITileService tileService)
    {
        _logger = logger;
        _tileService = tileService;
        dataLoaderService.SubscribeData<ItemObject>(OnItem);
    }

    private Task OnItem(ItemObject arg)
    {
        _items.Add(arg.Id, arg);
        return Task.CompletedTask;
    }


    private ItemObject? GetItemById(string id) => _items.GetValueOrDefault(id);

    private ItemObject? GetItemByCategory(string category)
    {
        var categoryItems = _items.Values.Where(i => i.Category == category).ToList();

        if (categoryItems.Count == 0)
        {
            _logger.LogWarning("No items found for category {Category}", category);
            return null;
        }

        return categoryItems.RandomElement();
    }

    public ItemGameObject BuildGameObject(string id, Point position)
    {
        ItemObject item = GetItemById(id) ?? GetItemByCategory(id);

        ItemGameObject itemGameObject = null;

        if (item == null)
        {
            _logger.LogError("Item not found for id or category {Id}", id);

            throw new InvalidOperationException($"Item not found for id or category {id}");
        }

        var (glyph, tile) = _tileService.GetTileWithEntry(item);

        itemGameObject = new ItemGameObject(position, glyph, true, true)
        {
            ItemId = item.Id,
            Name = item.Name
        };


        return itemGameObject;
    }

    public List<ItemGameObject> GetRandomItems(IEnumerable<IJsonRandomObject> randomBag)
    {
        var items = new List<ItemGameObject>();

        foreach (var random in randomBag)
        {
            var count = random.GetRandomValue();

            for (var i = 0; i < count; i++)
            {
                var item = BuildGameObject(random.Id, Point.None);

                _logger.LogTrace("Adding item {ItemId} to random items", item.ID);

                items.Add(item);
            }
        }

        return items;
    }

    public void AddItem(ItemObject item)
    {
        _logger.LogDebug("Adding item {ItemId}", item.Id);
        _items.Add(item.Id, item);
    }

    public void AddItemFeature(string id, Action<ItemFeatureContext> feature)
    {
        _itemFeatures.Add(id, feature);
    }
}
