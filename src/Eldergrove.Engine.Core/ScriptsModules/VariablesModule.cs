using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class VariablesModule
{
    private readonly IVariablesService _variablesService;

    public VariablesModule(IVariablesService variablesService)
    {
        _variablesService = variablesService;
    }

    [ScriptFunction("add_var")]
    public void AddVariableBuilder(string variableName, Func<object> builder)
    {
        _variablesService.AddVariableBuilder(variableName, builder);
    }

    [ScriptFunction("add_var")]
    public void AddVariable(string variableName, object value)
    {
        _variablesService.AddVariable(variableName, value);
    }

    [ScriptFunction("translate")]
    public string TranslateText(string text) => _variablesService.TranslateText(text);
}
