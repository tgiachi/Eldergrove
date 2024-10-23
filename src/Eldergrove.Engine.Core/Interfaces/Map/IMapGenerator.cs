using Eldergrove.Engine.Core.Data.Game;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.Types;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Interfaces.Map;

public interface IMapGenerator
{
    Task<GameMap> GenerateMapAsync(MapGeneratorObject generatorObject, Point mapSize, MapGeneratorType generatorType);

    Task PopulateMapAsync(GameMap map);
}
