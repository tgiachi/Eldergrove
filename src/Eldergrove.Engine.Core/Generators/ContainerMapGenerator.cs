using Eldergrove.Engine.Core.Attributes.Generators;
using Eldergrove.Engine.Core.Generators.Base;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Types;
using GoRogue.MapGeneration;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Generators;


[MapGenerator(MapGeneratorType.Container)]
public class ContainerMapGenerator : AbstractMapGenerator
{
    public ContainerMapGenerator(
        ILogger<ContainerMapGenerator> logger, ITileService tileService, IMapGenService mapGenService
    ) : base(logger, tileService, mapGenService)
    {
    }

    protected override IEnumerable<GenerationStep> GetGeneratorSteps() => DefaultAlgorithms.RectangleMapSteps();
}
