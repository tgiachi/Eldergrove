namespace Eldergrove.Engine.Core.Data.Scripts;

public class ScriptEngineExecutionResult
{
    public object Result { get; set; } = null!;
    public Exception? Exception { get; set; }
}
