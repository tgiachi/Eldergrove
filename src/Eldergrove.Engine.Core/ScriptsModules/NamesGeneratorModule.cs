using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class NamesGeneratorModule
{
    private readonly INameGeneratorService _nameGeneratorService;

    public NamesGeneratorModule(INameGeneratorService nameGeneratorService)
    {
        _nameGeneratorService = nameGeneratorService;
    }

    [ScriptFunction("name_add")]
    public void AddName(string type, string name)
    {
        _nameGeneratorService.AddName(type, name);
    }


    [ScriptFunction("name_add_from_file")]
    public void AddNamesFromFile(string type, string path)
    {
        var names = File.ReadAllLines(path);
        foreach (var name in names)
        {
            _nameGeneratorService.AddName(type, name);
        }
    }

    [ScriptFunction("name_generate")]
    public string GenerateName(string type) => _nameGeneratorService.GenerateName(type);
}
