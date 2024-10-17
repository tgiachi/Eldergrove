using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Data.Json.Random;

public class JsonRandomObject : IJsonRandomObject
{
    public string Id { get; set; }

    public int? Min { get; set; }

    public int? Max { get; set; }

    public string? Dice { get; set; }

    public JsonRandomObject(string id, int min, int max)
    {
        Id = id;
        Min = min;
        Max = max;
    }

    public JsonRandomObject()
    {
        Id = string.Empty;
        Min = null;
        Max = null;
    }

    public JsonRandomObject(int min, int max)
    {
        Id = string.Empty;
        Min = min;
        Max = max;
    }

    public JsonRandomObject(string dice)
    {
        Id = string.Empty;
        Dice = dice;
    }

    public override string ToString() => $"Id: {Id}, Min: {Min}, Max: {Max} Dice: {Dice}";
}
