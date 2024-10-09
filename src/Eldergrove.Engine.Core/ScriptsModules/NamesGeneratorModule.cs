using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class NamesGeneratorModule
{
    private readonly INameGeneratorService _nameGeneratorService;
    private readonly DirectoryConfig _directoryConfig;

    private readonly ILogger _logger;

    public NamesGeneratorModule(
        INameGeneratorService nameGeneratorService, DirectoryConfig directoryConfig, ILogger<NamesGeneratorModule> logger
    )
    {
        _nameGeneratorService = nameGeneratorService;
        _directoryConfig = directoryConfig;
        _logger = logger;
    }

    [ScriptFunction("name_add")]
    public void AddName(string type, string name)
    {
        _nameGeneratorService.AddName(type, name);
    }


    [ScriptFunction("name_add_from_file")]
    public void AddNamesFromFile(string type, string path)
    {
        var names = File.ReadAllLines(Path.Join(_directoryConfig.RootDirectory, path));
        foreach (var name in names)
        {
            _nameGeneratorService.AddName(type, name);
        }

        _logger.LogInformation("Added {Count} names of type {Type} from file {Path}", names.Length, type, path);
    }

    [ScriptFunction("name_generate")]
    public string GenerateName(string type) => _nameGeneratorService.GenerateName(type);
}
