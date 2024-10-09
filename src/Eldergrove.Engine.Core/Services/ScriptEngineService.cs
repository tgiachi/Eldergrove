using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
using NLua;
using NLua.Exceptions;
using Serilog;


namespace Eldergrove.Engine.Core.Services;

[AutostartService(1)]
public class ScriptEngineService : IScriptEngineService
{
    private readonly ILogger _logger = Log.ForContext<ScriptEngineService>();
    private readonly Lua _luaEngine;
    private readonly Dictionary<string, object> _scriptConstants = new();
    private readonly List<ScriptClassData> _scriptModules;
    private readonly DirectoryConfig _directoryConfig;
    private readonly IServiceProvider _container;
    private const string _fileExtension = "*.lua";
    public List<ScriptFunctionDescriptor> Functions { get; } = new();
    public Dictionary<string, object> ContextVariables { get; } = new();

    public ScriptEngineService(
        DirectoryConfig directoryConfig, List<ScriptClassData> scriptModules, IServiceProvider container
    )
    {
        _directoryConfig = directoryConfig;
        _scriptModules = scriptModules;
        _container = container;
        _luaEngine = new Lua();

        AddModulesDirectory();
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
            var fileName = Path.GetFileName(script);

            if (!fileName.StartsWith("_"))
            {
                await ExecuteFileAsync(script);
            }
        }


        if (ContextVariables.TryGetValue("bootstrap", out object? value) && value is LuaFunction bootstrap)
        {
            bootstrap.Call();
        }

    }

    private Task ScanScriptModulesAsync()
    {
        foreach (var module in _scriptModules)
        {
            _logger.Debug("Found script module {Module}", module.ClassType.Name);

            try
            {
                var obj = _container.GetService(module.ClassType);

                foreach (var scriptMethod in module.ClassType.GetMethods())
                {
                    var sMethodAttr = scriptMethod.GetCustomAttribute<ScriptFunctionAttribute>();

                    if (sMethodAttr == null)
                    {
                        continue;
                    }

                    ExtractFunctionDescriptor(sMethodAttr, scriptMethod);

                    _logger.Debug("Adding script method {M}", sMethodAttr.Alias ?? scriptMethod.Name);

                    _luaEngine[sMethodAttr.Alias ?? scriptMethod.Name] = CreateLuaEngineDelegate(obj, scriptMethod);
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
            _luaEngine.DoString(script);
        }
        catch (LuaException ex)
        {
            _logger.Error(ex, "Error executing script: {File}: {Formatted}", Path.GetFileName(file), FormatException(ex));
        }
    }

    private void ExtractFunctionDescriptor(ScriptFunctionAttribute attribute, MethodInfo methodInfo)
    {
        var descriptor = new ScriptFunctionDescriptor
        {
            FunctionName = attribute.Alias ?? methodInfo.Name,
            Help = attribute.Help,
            Parameters = new(),
            ReturnType = methodInfo.ReturnType.Name,
            RawReturnType = methodInfo.ReturnType
        };

        foreach (var parameter in methodInfo.GetParameters())
        {
            descriptor.Parameters.Add(
                new ScriptFunctionParameterDescriptor(
                    parameter.Name,
                    parameter.ParameterType.Name,
                    parameter.ParameterType
                )
            );
        }

        Functions.Add(descriptor);
    }

    public ScriptEngineExecutionResult ExecuteCommand(string command)
    {
        try
        {
            var result = new ScriptEngineExecutionResult
            {
                Result = _luaEngine.DoString(command)
            };

            return result;
        }
        catch (LuaException ex)
        {
            return new ScriptEngineExecutionResult() { Exception = ex };
        }
    }

    public void AddContextVariable(string name, object value)
    {
        _logger.Information("Adding context variable {Name} with value {Value}", name, value);
        _luaEngine[name] = value;
        ContextVariables[name] = value;
    }

    private static Delegate CreateLuaEngineDelegate(object obj, MethodInfo method)
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

    public async Task<string> GenerateDefinitionsAsync()
    {
        var luaDefinitions = new StringBuilder();

        luaDefinitions.AppendLine("-- Eldergrove Engine Lua Definitions");
        luaDefinitions.AppendLine();

        foreach (var constant in _scriptConstants)
        {
            luaDefinitions.AppendLine(
                $"-- {constant.Key}: {CSharpJsConverterUtils.ConvertCSharpTypeToTypeScript(constant.Value.GetType().Name)}"
            );
        }

        foreach (var constant in ContextVariables)
        {
            luaDefinitions.AppendLine(
                $"-- {constant.Key}: {CSharpJsConverterUtils.ConvertCSharpTypeToTypeScript(constant.Value.GetType().Name)}"
            );
        }

        luaDefinitions.AppendLine();



        foreach (var function in Functions)
        {
            if (!string.IsNullOrEmpty(function.Help))
            {
                luaDefinitions.AppendLine($"-- {function.Help}");
            }

            luaDefinitions.Append($"function {function.FunctionName}(");

            for (int i = 0; i < function.Parameters.Count; i++)
            {
                var param = function.Parameters[i];
                luaDefinitions.Append($"{param.ParameterName}");

                if (i < function.Parameters.Count - 1)
                {
                    luaDefinitions.Append(", ");
                }
            }

            luaDefinitions.AppendLine(") end");
            luaDefinitions.AppendLine();
        }

        return luaDefinitions.ToString();
    }


    private void AddModulesDirectory()
    {
        var modulesPath = Path.Combine(_directoryConfig[DirectoryType.Scripts]) + Path.DirectorySeparatorChar;
        var scriptModulePath = Path.Combine(_directoryConfig[DirectoryType.ScriptsModules]) + Path.DirectorySeparatorChar;

        _luaEngine.DoString(
            $@"
			-- Update the search path
			local module_folder = '{modulesPath}'
            local module_script_folder = '{scriptModulePath}'
			package.path = module_folder .. '?.lua;' .. package.path
            package.path = module_script_folder .. '?.lua;' .. package.path"
        );
    }

    private string FormatException(LuaException e)
    {
        var source = (string.IsNullOrEmpty(e.Source)) ? "<no source>" : e.Source.Substring(0, e.Source.Length - 2);
        return string.Format("{0}\nLua (at {2})", e.Message, string.Empty, source);
    }

    public Task StopAsync()
    {
        _luaEngine.Dispose();

        return Task.CompletedTask;
    }
}
