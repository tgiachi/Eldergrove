using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class MapModule
{
    private readonly IMapGenService _mapGenService;

    public MapModule(IMapGenService mapGenService)
    {
        _mapGenService = mapGenService;
    }


    [ScriptFunction("generate_map")]
    public async void GenerateMap()
    {
        await _mapGenService.GenerateMainMapAsync();
    }
}
