using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.GameObjects;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Services;

public class PropService : IPropService, IGameObjectFactory<PropGameObject>
{
    private readonly ILogger _logger;

    private readonly List<PropObject> _props = new();

    private readonly ITileService _tileService;

    public PropService(ILogger<PropService> logger, ITileService tileService)
    {
        _logger = logger;
        _tileService = tileService;
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


        gameObject = new PropGameObject(position, glyph, !tile.IsBlocking, tile.IsTransparent);


        return gameObject;
    }
}
