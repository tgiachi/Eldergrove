using System.Linq.Expressions;
using System.Reflection;
using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
using Jint;
using Serilog;
using Serilog.Core;

namespace Eldergrove.Engine.Core.Services;

public class ScriptEngineService : IScriptEngineService

{
    private readonly ILogger _logger = Log.ForContext<ScriptEngineService>();
    private readonly Jint.Engine _engine;
    private readonly Dictionary<string, object> _scriptConstants = new();

    private readonly List<ScriptClassData> _scriptModules = new();

    private readonly DirectoryConfig _directoryConfig;

    //private readonly IServiceProvider _container;

    private readonly string _fileExtension = "*.js";

    public List<ScriptFunctionDescriptor> Functions { get; } = new();

    public Dictionary<string, object> ContextVariables { get; } = new();
    public Task<string> GenerateTypeDefinitionsAsync() => throw new NotImplementedException();


    public ScriptEngineService(DirectoryConfig directoryConfig)
    {
        _directoryConfig = directoryConfig;
        _engine = new Jint.Engine(
            options =>
            {
                options.DebugMode(true);
                //options.TimeoutInterval(TimeSpan.FromSeconds(4));
                // Limit the memory to 4Gb
                options.LimitMemory(4_000_000_000);


                options.EnableModules(_directoryConfig[DirectoryType.ScriptsModules]);
                options.StringCompilationAllowed = true;
            }
        );
    }

    public async Task StartAsync()
    {
        await ScanScriptModulesAsync();
        var scriptsToLoad = Directory.GetFiles(
            _directoryConfig[DirectoryType.Scripts],
            _fileExtension,
            SearchOption.AllDirectories
        );

        foreach (var script in scriptsToLoad)
        {
            ExecuteFileAsync(script);
        }
    }

    private Task ScanScriptModulesAsync()
    {
        foreach (var module in _scriptModules)
        {
            _logger.Debug("Found script module {Module}", module.ClassType.Name);

            try
            {
                var obj = Activator.CreateInstance(module.ClassType);

                foreach (var scriptMethod in module.ClassType.GetMethods())
                {
                    var sMethodAttr = scriptMethod.GetCustomAttribute<ScriptFunctionAttribute>();

                    if (sMethodAttr == null)
                    {
                        continue;
                    }

                    ExtractFunctionDescriptor(sMethodAttr, scriptMethod);

                    _logger.Information("Adding script method {M}", sMethodAttr.Alias ?? scriptMethod.Name);

                    _engine.SetValue(
                        sMethodAttr.Alias ?? scriptMethod.Name,
                        CreateJsEngineDelegate(obj, scriptMethod)
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error during initialize script module {Alias}: {Ex}", module.ClassType, ex);
            }
        }

        return Task.CompletedTask;
    }

    public async Task ExecuteFileAsync(string file)
    {
        _logger.Information("Executing script: {File}", Path.GetFileName(file));
        try
        {
            var script = await File.ReadAllTextAsync(file);
            _engine.Execute(script);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error executing script: {File}", Path.GetFileName(file));
        }
    }

    private void ExtractFunctionDescriptor(ScriptFunctionAttribute attribute, MethodInfo methodInfo)
    {
        var descriptor = new ScriptFunctionDescriptor
        {
            FunctionName = attribute.Alias ?? methodInfo.Name,
            Help = attribute.Help,
            Parameters = new List<ScriptFunctionParameterDescriptor>(),
            ReturnType = methodInfo.ReturnType.Name,
            RawReturnType = methodInfo.ReturnType
        };

        foreach (var parameter in methodInfo.GetParameters())
        {
            descriptor.Parameters.Add(
                new ScriptFunctionParameterDescriptor(parameter.Name, parameter.ParameterType.Name, parameter.ParameterType)
            );
        }

        Functions.Add(descriptor);
    }

    public ScriptEngineExecutionResult ExecuteCommand(string command)
    {
        try
        {
            var result = new ScriptEngineExecutionResult { Result = _engine.Evaluate(command) };

            return result;
        }
        catch (Exception ex)
        {
            return new ScriptEngineExecutionResult() { Exception = ex };
        }
    }


    private void AddContextVariable(string name, object value)
    {
        _engine.SetValue(name, value);
        ContextVariables[name] = value;
    }

    private static Delegate CreateJsEngineDelegate(object obj, MethodInfo method)
    {
        return method.CreateDelegate(
            Expression.GetDelegateType(
                (from parameter in method.GetParameters() select parameter.ParameterType)
                .Concat(new[] { method.ReturnType })
                .ToArray()
            ),
            obj
        );
    }

    public Task StopAsync()
    {
        _engine.Dispose();

        return Task.CompletedTask;
    }
}
