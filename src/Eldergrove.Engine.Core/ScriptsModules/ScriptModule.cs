using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;
using NLua;


namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class ScriptModule
{
    private readonly IScriptEngineService _scriptEngineService;

    private readonly IDataLoaderService _dataLoaderService;
    public ScriptModule(IScriptEngineService scriptEngineService, IDataLoaderService dataLoaderService)
    {
        _scriptEngineService = scriptEngineService;
        _dataLoaderService = dataLoaderService;
    }

    [ScriptFunction("add_ctx")]
    public void AddContextVariable(string name, object value)
    {
        _scriptEngineService.AddContextVariable(name, value);
    }


    [ScriptFunction("load_json", "Load json file")]
    public void LoadJson(string path)
    {


    }


    [ScriptFunction("on_start")]
    public void RegisterBootstrap(LuaFunction function)
    {
        _scriptEngineService.AddContextVariable("bootstrap", function);
    }
}
