using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Interfaces.Services.Base;
using Eldergrove.Engine.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Eldergrove.Engine.Core.Manager;

public class EldergroveEngine : IEldergroveEngine
{
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    private IServiceProvider _serviceProvider;
    private readonly EldergroveOptions _options;
    private readonly DirectoryConfig _directoryConfig;

    private ILogger _logger;

    public EldergroveEngine(EldergroveOptions options)
    {
        _options = options;
        _directoryConfig = new DirectoryConfig(_options.RootDirectory);
        ConfigureLogger();
        RegisterServices();
    }

    private void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        _logger = Log.Logger.ForContext<IEldergroveEngine>();
        _serviceCollection.AddLogging(builder => builder.ClearProviders().AddSerilog());
    }

    private void RegisterServices()
    {
        _serviceCollection
            .AddDefaultJsonSettings()
            .AddSingleton(_directoryConfig)
            .AddSingleton<IScriptEngineService, ScriptEngineService>()
            .AddSingleton<IMessageBusService, MessageBusService>()
            .AddSingleton<IDataLoaderService, DataLoaderService>();
    }


    public async Task InitializeAsync()
    {
        var services = _serviceProvider.GetServices<IEldergroveService>();

        foreach (var service in services)
        {
            await service.StartAsync();
        }
    }

    public Task StartAsync()
    {
        _serviceProvider = _serviceCollection.BuildServiceProvider();

        return Task.CompletedTask;
    }
}
