using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Manager;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class EngineEventModule
{
    private readonly IEldergroveEngine _engine;

    public EngineEventModule(IEldergroveEngine engine)
    {
        _engine = engine;
    }


    [ScriptFunction("on_engine_start")]
    public void OnEngineStart(Action action)
    {
        _engine.AddOnEngineStart(action);
    }
}
