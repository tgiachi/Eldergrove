using NLua;

namespace Eldergrove.Engine.Core.Utils;

public static class ScriptUtils
{

    public static Dictionary<string, object> LuaTableToDictionary(LuaTable luaTable)
    {
        var dict = new Dictionary<string, object>();

        foreach (var key in luaTable.Keys)
        {
            dict[key.ToString()] = luaTable[key];
        }

        return dict;
    }


}
