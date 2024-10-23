using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.GameObject;
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
                }
            }
        }
    }
}
