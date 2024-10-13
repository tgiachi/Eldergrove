using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.Types;
using GoRogue.GameFramework;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IMapGenService
{
    GameMap CurrentMap { get; }

    List<TGameObject> GetEntities<TGameObject>(MapLayerType layerType) where TGameObject : IGameObject;

    void AddFabric(MapFabricObject fabric);

    Task<GameMap> GenerateMapAsync();

}
