using Eldergrove.Engine.Core.Attributes.Scripts;
using GoRogue.DiceNotation;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class RandomModule
{
    [ScriptFunction("rnd_int", "Returns a random integer value.")]
    public int RandomInt(int min, int max) => new Random().Next(min, max);


    [ScriptFunction("rnd_dice", "Rolls a dice expression.")]
    public int RandomDice(string expression)
    {
        var diceExpression = Dice.DiceParser.Parse(expression);
        return diceExpression.Roll();
    }


    [ScriptFunction("rnd_bool", "Returns a random boolean value.")]
    public bool RandomBool() => new Random().Next(0, 2) == 1;
}
