using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Data;

[DataLoaderType("skill_def")]
public class JsonSkillsObject : IJsonSkillsObject
{
    public string Id { get; set; }
    public JsonRandomObject Health { get; set; }

    public JsonRandomObject Gold { get; set; } = new(1, 10);
}
