using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;
using NLua;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class ItemModule
{
    private readonly IItemService _itemService;

    public ItemModule(IItemService itemService)
    {
        _itemService = itemService;
    }


    [ScriptFunction("add_item_feature", "Adds an item feature to the game")]
    public void AddItemFeature(string featureName, LuaFunction callBack)
    {
        _itemService.AddItemFeature(featureName, (ctx) => callBack.Call(ctx));
    }
}
