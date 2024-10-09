using Eldergrove.Engine.Core.Interfaces.Json;

namespace Eldergrove.Engine.Core.Utils;

public static class RandomUtils
{
    public static int RandomRange(int min, int max) => System.Random.Shared.Next(min, max);

    public static int GetRandom(IJsonRandomObject randomObject) => RandomRange(randomObject.Min, randomObject.Max);


    public static int GetRandomValue(this IJsonRandomObject randomObject) => RandomRange(randomObject.Min, randomObject.Max);
}
