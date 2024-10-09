using Eldergrove.Engine.Core.Data.Json.Random;

namespace Eldergrove.Engine.Core.Interfaces.Json;

public interface IJsonContainerObject
{
    List<JsonRandomObject>? Container { get; set; }
}
