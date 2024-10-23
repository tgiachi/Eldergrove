using Eldergrove.Engine.Core.Interfaces.Map;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Generators.Base;

public abstract class AbstractMapGenerator : IMapGenerator
{
    protected ILogger _logger;

    protected AbstractMapGenerator(ILogger logger)
    {
        _logger = logger;
    }

    public virtual Task GenerateMapAsync()
    {
        return Task.CompletedTask;
    }
}
