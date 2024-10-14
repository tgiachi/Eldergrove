using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.Bars;
using Eldergrove.Engine.Core.Data.Json.Colors;
using Eldergrove.Engine.Core.Data.Json.Dialogs;
using Eldergrove.Engine.Core.Data.Json.Items;
using Eldergrove.Engine.Core.Data.Json.Keybinding;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Data.Json.Names;
using Eldergrove.Engine.Core.Data.Json.Npcs;
using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.Data.Json.Texts;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Interfaces.Services.Base;
using Eldergrove.Engine.Core.KeybindingActions;
using Eldergrove.Engine.Core.ScriptsModules;
using Eldergrove.Engine.Core.Services;
using Eldergrove.Engine.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Eldergrove.Engine.Core.Manager;

public class EldergroveEngine : IEldergroveEngine
{
    private readonly List<Action> _onEngineStart = new();
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    private ServiceProvider _serviceProvider;
    private readonly EldergroveOptions _options;
    private readonly DirectoryConfig _directoryConfig;

    private Func<IServiceCollection, IServiceCollection>? _serviceCollectionDelegate;

    public INpcService NpcService => GetService<INpcService>();

    private ILogger _logger;

    public EldergroveEngine(
        EldergroveOptions options, Func<IServiceCollection, IServiceCollection> serviceCollectionDelegate = null
    )
    {
        _options = options;

        _serviceCollectionDelegate = serviceCollectionDelegate;

        ConfigureLogger();
        _directoryConfig = new DirectoryConfig(_options.RootDirectory);

        _logger.Debug("Root directory set to {RootDirectory}", _options.RootDirectory);

        RegisterServices();
        RegisterScriptModules();
        RegisterDataLoaders();

        _serviceCollectionDelegate?.Invoke(_serviceCollection);
    }

    private void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.EventBus()
            .CreateLogger();

        _logger = Log.Logger.ForContext<IEldergroveEngine>();
        _serviceCollection.AddLogging(builder => builder.ClearProviders().AddSerilog());
    }


    private void RegisterScriptModules()
    {
        _serviceCollection
            .RegisterScriptModule<LoggerModule>()
            .RegisterScriptModule<ScriptModule>()
            .RegisterScriptModule<ActionCommandModule>()
            .RegisterScriptModule<NamesGeneratorModule>()
            .RegisterScriptModule<ColorsModule>()
            .RegisterScriptModule<RandomModule>()
            .RegisterScriptModule<EngineEventModule>()
            .RegisterScriptModule<EventsModule>()
            .RegisterScriptModule<TileServiceModule>()
            .RegisterScriptModule<NpcModule>()
            .RegisterScriptModule<MapModule>()
            .RegisterScriptModule<VariablesModule>()
            .RegisterScriptModule<TextServiceModule>()
            ;
    }

    private void RegisterDataLoaders()
    {
        _serviceCollection
            .AddDataLoaderType<TileSetObject>()
            .AddDataLoaderType<KeybindingObject>()
            .AddDataLoaderType<ColorObject>()
            .AddDataLoaderType<PropObject>()
            .AddDataLoaderType<NamesObject>()
            .AddDataLoaderType<ItemObject>()
            .AddDataLoaderType<NpcObject>()
            .AddDataLoaderType<MapFabricObject>()
            .AddDataLoaderType<MapGeneratorObject>()
            .AddDataLoaderType<DialogObject>()
            .AddDataLoaderType<BarObject>()
            .AddDataLoaderType<BarDefinition>()
            .AddDataLoaderType<TextObject>()
            ;
    }

    private void RegisterServices()
    {
        _serviceCollection
            .AddDefaultJsonSettings()
            .AddSingleton(_directoryConfig)
            .AddSingleton<IEldergroveEngine>(this)
            .AddEldergroveService<IScriptEngineService, ScriptEngineService>()
            .AddEldergroveService<IMessageBusService, MessageBusService>()
            .AddEldergroveService<IDataLoaderService, DataLoaderService>()
            .AddEldergroveService<IKeyActionCommandService, KeyActionCommandService>()
            .AddEldergroveService<INameGeneratorService, NameGeneratorService>()
            .AddEldergroveService<IColorService, ColorService>()
            .AddEldergroveService<IPropService, PropService>()
            .AddEldergroveService<ITileService, TileService>()
            .AddEldergroveService<IItemService, ItemService>()
            .AddEldergroveService<IVersionService, VersionService>()
            .AddEldergroveService<IEventDispatcherService, EventDispatcherService>()
            .AddEldergroveService<INpcService, NpcService>()
            .AddEldergroveService<IMapGenService, MapGenService>()
            .AddEldergroveService<ISchedulerService, SchedulerService>()
            .AddEldergroveService<IVariablesService, VariableService>()
            ;

        // Register default keybinding actions
        _serviceCollection
            .AddKeybindingAction<PlayerMoveUp>()
            .AddKeybindingAction<PlayerMoveDown>()
            .AddKeybindingAction<PlayerMoveLeft>()
            .AddKeybindingAction<PlayerMoveRight>()
            ;
    }


    public async Task InitializeAsync()
    {
        foreach (var line in HeaderUtils.EldergroveHeader)
        {
            _logger.Information(line);
        }

        _logger.Information("Version: {Version}", _serviceProvider.GetService<IVersionService>().GetVersion());

        var serviceToLoad = _serviceProvider.GetService<List<AutostartServiceData>>();

        foreach (var s in serviceToLoad.OrderBy(s => s.Order))
        {
            var service = _serviceProvider.GetService(s.ServiceType) as IEldergroveService;

            if (service == null)
            {
                //_logger.Error("Failed to load service {ServiceType}", s.ServiceType);
                continue;
            }


            Log.Logger.Debug("Starting service {ServiceType}", s.ServiceType.Name);
            await service.StartAsync();
        }

        foreach (var action in _onEngineStart)
        {
            action();
        }


        GetService<IMessageBusService>().Publish(new EngineReadyEvent());
    }

    public Task StartAsync()
    {
        _serviceProvider = _serviceCollection.BuildServiceProvider();


        return Task.CompletedTask;
    }

    public TService GetService<TService>() where TService : class => _serviceProvider.GetService<TService>();

    public INpcService GetNpcService() => GetService<INpcService>();

    public void AddOnEngineStart(Action action)
    {
        _onEngineStart.Add(action);
    }
}
