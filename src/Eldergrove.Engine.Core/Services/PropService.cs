using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

[AutostartService]
public class PropService : IPropService
{
    private readonly ILogger _logger;

    private readonly List<PropObject> _props = new();

    private readonly ITileService _tileService;

    private readonly IItemService _itemService;


    public PropService(
        ILogger<PropService> logger, ITileService tileService, IItemService itemService, IDataLoaderService dataLoaderService
    )
    {
        _logger = logger;
        _tileService = tileService;
        _itemService = itemService;
        dataLoaderService.SubscribeData<PropObject>(
            o =>
            {
                AddProp(o);

                return Task.CompletedTask;
            }
        );
    }

    public Task StartAsync() => Task.CompletedTask;

    public Task StopAsync() => Task.CompletedTask;

    public void AddProp(PropObject prop)
    {
        _logger.LogInformation("Adding prop {PropId}", prop.Id);
        _props.Add(prop);
    }

    public PropObject? GetPropById(string id) => _props.FirstOrDefault(p => p.Id == id);


    private PropObject? GetPropByCategory(string category)
    {
        var categoryProps = _props.Where(p => p.Category == category).ToList();


        if (categoryProps.Count == 0)
        {
            _logger.LogWarning("No props found for category {Category}", category);
            return null;
        }

        return categoryProps.RandomElement();
    }

    public PropGameObject BuildGameObject(string idOrCategory, Point position)
    {
        PropObject prop = null;
        PropGameObject gameObject = null;


        prop = GetPropById(idOrCategory) ?? GetPropByCategory(idOrCategory);


        if (prop == null)
        {
            _logger.LogWarning("No prop found for id or category {IdOrCategory}", idOrCategory);
            throw new KeyNotFoundException($"No prop found for id or category {idOrCategory}");
        }

        var (glyph, tile) = _tileService.GetTileWithEntry(prop);


        gameObject = new PropGameObject(position, glyph, false, prop.IsTransparent)
        {
            CanDestroy = prop.IsDestructible
        };


        if (prop.Name != null)
        {
            gameObject.Name = prop.Name;
        }


        if (prop.Door != null)
        {
            var openedTile = _tileService.GetTile(prop.Door.On);
            var closedTile = _tileService.GetTile(prop.Door.Off);

            gameObject.GoRogueComponents.Add(new DoorComponent(closedTile, openedTile));
        }

        if (prop.Container != null)
        {
            var items = _itemService.GetRandomItems(prop.Container);
            gameObject.GoRogueComponents.Add(new ItemsContainerComponent(items.ToList()));
        }


        _tileService.BuildTileAnimation(gameObject, tile);


        return gameObject;
    }
}
