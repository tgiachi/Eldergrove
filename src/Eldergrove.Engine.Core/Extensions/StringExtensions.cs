using Eldergrove.Engine.Core.Utils;

namespace Eldergrove.Engine.Core.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }


    public static int ParseTileSymbol(this string str)
    {
        if (str.StartsWith("##"))
        {
            return str[2];
        }


        if (str.StartsWith("!!"))
        {
            var symbol = str[2..];

            if (symbol.Contains('-'))
            {
                var firstPart = int.Parse(symbol.Split('-')[0]);
                var secondPart = int.Parse(symbol.Split('-')[1]);

                return RandomUtils.RandomRange(firstPart, secondPart);
            }

            return int.Parse(str[2].ToString());
        }

        return str[0];
    }
}
