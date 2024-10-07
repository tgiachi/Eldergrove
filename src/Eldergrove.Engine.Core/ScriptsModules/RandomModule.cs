using Eldergrove.Engine.Core.Attributes.Scripts;
using GoRogue.DiceNotation;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class RandomModule
{
    [ScriptFunction("random_int", "Returns a random integer value.")]
    public int RandomInt(int min, int max) => new Random().Next(min, max);


    [ScriptFunction("random_dice", "Rolls a dice expression.")]
    public int RandomDice(string expression)
    {
        var diceExpression = Dice.DiceParser.Parse(expression);
        return diceExpression.Roll();
    }


    [ScriptFunction("random_bool", "Returns a random boolean value.")]
    public bool RandomBool() => new Random().Next(0, 2) == 1;
}
