using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Data;

public class JsonSkillsObject : IJsonSkillsObject
{
    public JsonRandomObject Health { get; set; }
}
