using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Maps;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IMapGenService
{
    GameMap CurrentMap { get; }




    void AddFabric(MapFabricObject fabric);

    Task<GameMap> GenerateMapAsync();

}
