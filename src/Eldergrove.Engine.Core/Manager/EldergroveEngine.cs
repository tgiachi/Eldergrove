using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.Colors;
using Eldergrove.Engine.Core.Data.Json.Items;
using Eldergrove.Engine.Core.Data.Json.Keybinding;
using Eldergrove.Engine.Core.Data.Json.Names;
using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Interfaces.Services.Base;
using Eldergrove.Engine.Core.ScriptsModules;
using Eldergrove.Engine.Core.Services;
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

    private ILogger _logger;

    public EldergroveEngine(EldergroveOptions options)
    {
        _options = options;

        ConfigureLogger();
        _directoryConfig = new DirectoryConfig(_options.RootDirectory);

        _logger.Debug("Root directory set to {RootDirectory}", _options.RootDirectory);

        RegisterServices();
        RegisterScriptModules();
        RegisterDataLoaders();
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
            .AddEldergroveService<IActionCommandService, ActionCommandService>()
            .AddEldergroveService<INameGeneratorService, NameGeneratorService>()
            .AddEldergroveService<IColorService, ColorService>()
            .AddEldergroveService<IPropService, PropService>()
            .AddEldergroveService<ITileService, TileService>()
            ;
    }


    public async Task InitializeAsync()
    {
        var serviceToLoad = _serviceProvider.GetService<List<AutostartServiceData>>();

        foreach (var s in serviceToLoad)
        {
            var service = _serviceProvider.GetService(s.ServiceType) as IEldergroveService;

            if (service == null)
            {
                _logger.Error("Failed to load service {ServiceType}", s.ServiceType);
                continue;
            }


            Log.Logger.Information("Starting service {ServiceType}", s.ServiceType.Name);
            await service.StartAsync();
        }
    }

    public Task StartAsync()
    {
        _serviceProvider = _serviceCollection.BuildServiceProvider();

        foreach (var action in _onEngineStart)
        {
            action();
        }

        return Task.CompletedTask;
    }

    public TService GetService<TService>() where TService : class => _serviceProvider.GetService<TService>();

    public void AddOnEngineStart(Action action)
    {
        _onEngineStart.Add(action);
    }
}
