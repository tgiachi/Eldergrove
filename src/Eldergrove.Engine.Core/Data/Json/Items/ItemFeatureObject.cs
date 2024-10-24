using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Items;

public class ItemFeatureObject : IJsonItemFeatureObject
{
    public string Id { get; set; }
    public object[] Params { get; set; } = [];
}
