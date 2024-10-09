namespace Eldergrove.Engine.Core.Utils;

public static class RandomUtils
{
    public static int RandomRange(int min, int max) => System.Random.Shared.Next(min, max);


}
