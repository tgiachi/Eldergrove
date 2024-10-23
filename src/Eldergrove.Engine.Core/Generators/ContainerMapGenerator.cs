using Eldergrove.Engine.Core.Generators.Base;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Generators;

public class ContainerMapGenerator : AbstractMapGenerator
{
    public ContainerMapGenerator(
        ILogger<ContainerMapGenerator> logger, ITileService tileService, IMapGenService mapGenService
    ) : base(logger, tileService, mapGenService)
    {
    }
}
