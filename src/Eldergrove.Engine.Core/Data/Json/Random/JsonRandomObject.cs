using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Random;

public class JsonRandomObject : IJsonRandomObject
{
    public string Id { get; set; }

    public int Min { get; set; }

    public int Max { get; set; }

    public JsonRandomObject(string id, int min, int max)
    {
        Id = id;
        Min = min;
        Max = max;
    }

    public JsonRandomObject()
    {
        Id = string.Empty;
        Min = 0;
        Max = 0;
    }

    public JsonRandomObject(int min, int max)
    {
        Id = string.Empty;
        Min = min;
        Max = max;
    }

    public override string ToString() => $"Id: {Id}, Min: {Min}, Max: {Max}";
}
