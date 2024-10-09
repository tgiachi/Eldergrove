using System.Text.Json;
using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Data.Json.Npcs;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Utils;
using NLua;


namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class NpcModule
{
    private readonly INpcService _npcService;

    private readonly JsonSerializerOptions _serializerOptions;

    public NpcModule(INpcService npcService, JsonSerializerOptions serializerOptions)
    {
        _npcService = npcService;
        _serializerOptions = serializerOptions;
    }


    [ScriptFunction("add_npc", "Adds an npc to the game")]
    public void AddNpc(LuaTable npcData)
    {
        var json = JsonSerializer.Serialize(ScriptUtils.LuaTableToDictionary(npcData), _serializerOptions);
        var obj = JsonSerializer.Deserialize<NpcObject>(json, _serializerOptions);
        _npcService.AddNpc(obj);
    }

    [ScriptFunction("build_npc", "Builds an npc game object")]
    public NpcGameObject BuildGameObject(string idOrCategory, int x, int y) =>
        _npcService.BuildGameObject(idOrCategory, new(x, y));


    [ScriptFunction("add_brain", "Adds a brain to an npc")]
    public void AddBrain(string id, LuaFunction brain)
    {
        _npcService.AddBrain(id, ctx =>
        {
            brain.Call(ctx);
        });
    }
}
