using Eldergrove.Engine.Core.Data.Json.Random;

namespace Eldergrove.Engine.Core.Interfaces.Json;

public interface IJsonSkillsObject : IJsonDataObject
{
    JsonRandomObject Health { get; set; }

    JsonRandomObject Gold { get; set; }
}
