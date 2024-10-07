using Eldergrove.Engine.Core.Attributes.Scripts;
using GoRogue.DiceNotation;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class RandomModule
{
    [ScriptFunction("random_int")]
    public int RandomInt(int min, int max) => new Random().Next(min, max);


    [ScriptFunction("random_dice")]
    public int RandomDice(string expression)
    {
        var diceExpression = Dice.DiceParser.Parse(expression);
        return diceExpression.Roll();
    }
}
