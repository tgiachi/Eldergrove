using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class PropModule
{
    private readonly IPropService _propService;

    public PropModule(IPropService propService)
    {
        _propService = propService;
    }

    [ScriptFunction("prop_new")]
    public PropGameObject? BuildGameObject(string idOrCategory, int x, int y) =>
        _propService.BuildGameObject(idOrCategory, new(x, y));
}
