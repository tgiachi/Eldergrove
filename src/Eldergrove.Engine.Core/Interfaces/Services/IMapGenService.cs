using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.GameObjects;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.Types;
using GoRogue.GameFramework;
using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IMapGenService :  IGameObjectFactory<MapFabricObject>
{
    GameMap CurrentMap { get; }

    List<TGameObject> GetEntities<TGameObject>(MapLayerType layerType) where TGameObject : IGameObject;

    public GeneratedFabricLayersData GenerateFabricAsync(
        MapFabricObject fabric, GlyphTileEntry wall, GlyphTileEntry floor, GameMap map,
        Point? startingPoint = null
    );

    void AddFabric(MapFabricObject fabric);

    Task<GameMap> GenerateMainMapAsync();
}
