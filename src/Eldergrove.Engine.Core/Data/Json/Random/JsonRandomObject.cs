using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Random;

public class JsonRandomObject : IJsonRandomObject
{
    public string Id { get; set; }

    public int Min { get; set; }

    public int Max { get; set; }

    public override string ToString() => $"Id: {Id}, Min: {Min}, Max: {Max}";
}
