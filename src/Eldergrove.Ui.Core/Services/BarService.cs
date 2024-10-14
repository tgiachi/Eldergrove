using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.Json.Bars;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Ui.Core.Data.Internal;
using Eldergrove.Ui.Core.Interfaces;
using GoRogue.Messaging;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Ui.Core.Services;

[AutostartService]
public class BarService : IBarService, ISubscriber<TickEvent>
{
    private readonly ILogger _logger;


    private readonly IVariablesService _variablesService;

    private readonly IColorService _colorService;

    private readonly Dictionary<string, BarObject> _bars = new();

    private readonly Dictionary<string, BarDefinition> _barDefinitions = new();

    private readonly Dictionary<string, List<BarColoredObject>> _coloredBars = new();

    private readonly Dictionary<string, Action<List<BarColoredObject>>> callbacks = new();


    public BarService(
        ILogger<BarService> logger, IDataLoaderService dataLoaderService, IColorService colorService,
        IMessageBusService messageBusService, IVariablesService variablesService
    )
    {
        _colorService = colorService;
        _variablesService = variablesService;

        dataLoaderService.SubscribeData<BarObject>(OnBarObjects);
        dataLoaderService.SubscribeData<BarDefinition>(OnBarDefinitions);

        _logger = logger;

        messageBusService.Subscribe(this);
    }

    private Task OnBarDefinitions(BarDefinition arg)
    {
        _logger.LogDebug("Loaded bar definition {Id}", arg.Id);

        _barDefinitions[arg.Id] = arg;
        return Task.CompletedTask;
    }

    private Task OnBarObjects(BarObject arg)
    {
        AddBarObject(arg);
        return Task.CompletedTask;
    }

    public void AddBarObject(BarObject barObject)
    {
        _logger.LogDebug("Loaded bar {Id}", barObject.Id);

        _bars[barObject.Id] = barObject;
    }

    public void BuildBar(string id, Action<List<BarColoredObject>> callback)
    {
        if (!_barDefinitions.TryGetValue(id, out var barDefinition))
        {
            _logger.LogError("Bar defintion {Id} not found", id);

            throw new KeyNotFoundException($"Bar defintion {id} not found");
        }

        var bars = new List<BarColoredObject>();

        foreach (var barsId in barDefinition.BarIds)
        {
            var sourceBar = _bars[barsId];

            var bar = new BarColoredObject()
            {
                Background = _colorService.GetColor(sourceBar.Background),
                Foreground = _colorService.GetColor(sourceBar.Foreground),
                SourceText = sourceBar.Text,
                RenderedText = _variablesService.TranslateText(sourceBar.Text)
            };


            bars.Add(bar);
        }

        _coloredBars[id] = bars;

        callbacks[id] = callback;

        callback(bars);
    }

    public string GetBarFromPosition(BarPositionType positionType)
    {
        return _barDefinitions.Values.FirstOrDefault(x => x.Position == positionType).Id;
    }

    public void Handle(TickEvent message)
    {
        foreach (var bar in _coloredBars)
        {
            foreach (var barColoredObject in bar.Value)
            {
                barColoredObject.RenderedText = _variablesService.TranslateText(barColoredObject.SourceText);
            }

            callbacks[bar.Key](bar.Value);
        }
    }
}
