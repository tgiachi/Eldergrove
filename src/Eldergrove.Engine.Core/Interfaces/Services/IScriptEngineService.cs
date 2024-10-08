using Eldergrove.Engine.Core.Data.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services.Base;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IScriptEngineService : IEldergroveService
{
    Task ExecuteFileAsync(string file);

    ScriptEngineExecutionResult ExecuteCommand(string command);

    List<ScriptFunctionDescriptor> Functions { get; }

    Dictionary<string, object> ContextVariables { get; }

    Task<string> GenerateDefinitionsAsync();

    void AddContextVariable(string name, object value);

    TVar? GetContextVariable<TVar>(string name, bool throwIfNotFound = true) where TVar : class;
}
