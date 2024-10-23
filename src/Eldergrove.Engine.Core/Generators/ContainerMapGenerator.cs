using Eldergrove.Engine.Core.Generators.Base;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Generators;

public class ContainerMapGenerator : AbstractMapGenerator
{
    public ContainerMapGenerator(ILogger<ContainerMapGenerator> logger) : base(logger)
    {
    }
}
