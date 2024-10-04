using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Services;
using Serilog;

namespace Eldergrove.Engine.Core.Services;

public class DataLoaderService : IDataLoaderService
{
    private readonly ILogger _logger = Log.ForContext<DataLoaderService>();

    private readonly Dictionary<string, List<object>> _data = new();

    private readonly string _typeToken = "$type";

    private readonly DirectoryConfig _directoryConfig;

    public DataLoaderService(DirectoryConfig directoryConfig)
    {
        _directoryConfig = directoryConfig;
    }


    public Task StartAsync()
    {
        return Task.CompletedTask;
    }

    public Task StopAsync() => Task.CompletedTask;
}
