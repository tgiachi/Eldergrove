using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Json.Items;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

[AutostartService(1)]
public class ItemService : IItemService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, ItemObject> _items = new();

    public ItemService(ILogger<ItemService> logger, IDataLoaderService dataLoaderService)
    {
        _logger = logger;
        dataLoaderService.SubscribeData<ItemObject>(OnItem);
    }

    private Task OnItem(ItemObject arg)
    {
        _items.Add(arg.Id, arg);
        return Task.CompletedTask;
    }
}
