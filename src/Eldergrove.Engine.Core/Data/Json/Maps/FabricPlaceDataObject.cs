using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Data.Json.Maps;

public class FabricPlaceDataObject : JsonRandomObject
{
    public MapPlacementStrategyType Placement { get; set; } = MapPlacementStrategyType.Random;

    public MapGridObject? Grid { get; set; }


}
