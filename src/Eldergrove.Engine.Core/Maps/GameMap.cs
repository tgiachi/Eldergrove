using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Types;
using GoRogue.GameFramework;
using SadRogue.Integration.Maps;
using SadRogue.Primitives;
using SadRogue.Primitives.SpatialMaps;

namespace Eldergrove.Engine.Core.Maps;

public class GameMap : RogueLikeMap
{
    public MapGeneratorType GeneratorType { get; set; }

    private readonly Dictionary<MapLayerType, List<IGameObject>> _entities = new();

    public GameMap(int width, int height, DefaultRendererParams? defaultRendererParams)
        : base(width, height, defaultRendererParams, Enum.GetValues<MapLayerType>().Length, Distance.Euclidean)
    {
        foreach (var layerType in Enum.GetValues<MapLayerType>())
        {
            _entities.Add(layerType, []);
        }

        ObjectAdded += OnObjectAdded;
    }

    private void OnObjectAdded(object? sender, ItemEventArgs<IGameObject> e)
    {
        if (e.Item.Layer > (int)MapLayerType.Terrain)
        {
            _entities[(MapLayerType)e.Item.Layer].Add(e.Item);
        }
    }

    public void AddEntities(params IGameObject[] entities)
    {
        foreach (var entity in entities)
        {
            AddEntity(entity);
        }
    }

    public IEnumerable<TEntity> GetEntitiesFromLayer<TEntity>(MapLayerType layerType) where TEntity : IGameObject =>
        _entities[layerType].OfType<TEntity>();
}
