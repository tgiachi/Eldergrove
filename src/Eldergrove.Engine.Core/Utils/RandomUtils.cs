using Eldergrove.Engine.Core.Interfaces.Json;
using GoRogue.DiceNotation;
using GoRogue.Random;

namespace Eldergrove.Engine.Core.Utils;

public static class RandomUtils
{
    public static int RandomRange(int min, int max) => GlobalRandom.DefaultRNG.NextInt(min, max);

    public static int GetRandom(IJsonRandomObject randomObject)
    {
        return randomObject.Dice != null ? Dice.Roll(randomObject.Dice) : GetRandomValue(randomObject);
    }

    public static int GetRandomValue(this IJsonRandomObject randomObject)
    {
        if (randomObject.Min != null || randomObject.Max != null)
        {
            return RandomRange(randomObject.Min.Value, randomObject.Max.Value);
        }

        if (randomObject.Dice != null)
        {
            return Dice.Roll(randomObject.Dice);
        }

        throw new ArgumentException("Random object must have a Min/Max or Dice value");
    }

    public static IEnumerable<int> GetRandomValueAsRange(this IJsonRandomObject randomObject) => Enumerable.Range( 0, GetRandomValue(randomObject));
}
