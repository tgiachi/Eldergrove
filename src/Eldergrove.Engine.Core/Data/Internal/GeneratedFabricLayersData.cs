using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Types;
using GoRogue.GameFramework;

namespace Eldergrove.Engine.Core.Data.Internal;

public class GeneratedFabricLayersData
{
    public Dictionary<MapLayerType, List<IGameObject>> Layers { get; set; } = new();

    public MapFabricObject Fabric { get; set; }


    public int Width => Fabric.Width;

    public int Height => Fabric.Height;

    public int Area => Fabric.Area;


    public GeneratedFabricLayersData()
    {
        foreach (var layerType in Enum.GetValues<MapLayerType>())
        {
            Layers.Add(layerType, []);
        }
    }

    public void AddLayer(MapLayerType layerType, params IGameObject[] layer)
    {
        Layers[layerType].AddRange(layer);
    }


    public List<IGameObject> this[MapLayerType index] => Layers[index];


}
