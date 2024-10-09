using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;
using NLua;


namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class ScriptModule
{
    private readonly IScriptEngineService _scriptEngineService;

    public ScriptModule(IScriptEngineService scriptEngineService)
    {
        _scriptEngineService = scriptEngineService;
    }

    [ScriptFunction("add_ctx")]
    public void AddContextVariable(string name, object value)
    {
        _scriptEngineService.AddContextVariable(name, value);
    }


    [ScriptFunction("on_start")]
    public void RegisterBootstrap(LuaFunction function)
    {
        _scriptEngineService.AddContextVariable("bootstrap", function);
    }
}
