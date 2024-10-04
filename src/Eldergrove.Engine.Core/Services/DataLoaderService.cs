using Eldergrove.Engine.Core.Interfaces.Services;
using Serilog;

namespace Eldergrove.Engine.Core.Services;

public class DataLoaderService : IDataLoaderService
{
    private readonly ILogger _logger = Log.ForContext<DataLoaderService>();
}
