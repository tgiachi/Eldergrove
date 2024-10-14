using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.State;

namespace Eldergrove.Engine.Core.Extensions;

public static class TranslateExtension
{
    public static string VarTranslate(this string value)
    {
        if (EldergroveState.Engine == null)
        {
            throw new InvalidOperationException("Engine is not initialized");
        }


        return EldergroveState.Engine.GetService<IVariablesService>().TranslateText(value);
    }
}
