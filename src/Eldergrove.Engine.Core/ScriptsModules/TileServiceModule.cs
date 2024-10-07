using System.Text.Json;
using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.Services;
using Jint.Native;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class TileServiceModule
{
    private readonly ITileService _tileService;

    private readonly JsonSerializerOptions _serializerOptions;

    public TileServiceModule(ITileService tileService, JsonSerializerOptions serializerOptions)
    {
        _tileService = tileService;
        _serializerOptions = serializerOptions;
    }



    [ScriptFunction("add_tile")]
    public void AddTile(JsValue tileData)
    {
        var json = tileData.ToString();

        var obj = JsonSerializer.Deserialize<TileEntry>(json, _serializerOptions);

        _tileService.AddTile(obj);
    }
}