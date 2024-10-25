using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Types;
using GoRogue.GameFramework;
using SadRogue.Integration.Maps;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Maps;

public class GameMap : RogueLikeMap
{
    public MapGeneratorType GeneratorType { get; set; }


    public Dictionary<MapLayerType, List<IGameObject>> Entities { get; set; } = new();

    public GameMap(int width, int height, DefaultRendererParams? defaultRendererParams)
        : base(width, height, defaultRendererParams, Enum.GetValues<MapLayerType>().Length, Distance.Euclidean)
    {
        foreach (var layerType in Enum.GetValues<MapLayerType>())
        {
            Entities.Add(layerType, new List<IGameObject>());
        }
    }

    public void AddGeneratedFabricLayersData(params GeneratedFabricLayersData[] data)
    {
        foreach (var generatedFabricLayersData in data)
        {
            AddGeneratedFabricLayersData(generatedFabricLayersData);
        }
    }

    public void AddGeneratedFabricLayersData(GeneratedFabricLayersData data)
    {
        foreach (var layerType in Enum.GetValues<MapLayerType>())
        {
            foreach (var gameObject in data[layerType])
            {
                if (gameObject is TerrainGameObject terrainGameObject)
                {
                    SetTerrain(terrainGameObject);
                }
                else
                {
                    AddEntity(gameObject);

                    Entities[layerType].Add(gameObject);
                }
            }
        }
    }
}
