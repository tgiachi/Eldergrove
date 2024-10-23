using Eldergrove.Engine.Core.Types;
using SadRogue.Integration.Maps;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Maps;

public class GameMap : RogueLikeMap
{

    public MapGeneratorType GeneratorType { get; set; }

    public GameMap(int width, int height, DefaultRendererParams? defaultRendererParams)
        : base(width, height, defaultRendererParams, Enum.GetValues<MapLayerType>().Length - 1, Distance.Euclidean)
    {
    }
}
