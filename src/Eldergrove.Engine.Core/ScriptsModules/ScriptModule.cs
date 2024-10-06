using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class ScriptModule
{
    private readonly IScriptEngineService _scriptEngineService;

    public ScriptModule(IScriptEngineService scriptEngineService)
    {
        _scriptEngineService = scriptEngineService;
    }

    [ScriptFunction("add_ctx_var")]
    public void AddContextVariable(string name, object value)
    {
        _scriptEngineService.AddContextVariable(name, value);
    }
}
