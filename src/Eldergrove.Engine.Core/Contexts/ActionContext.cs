using Eldergrove.Engine.Core.Interfaces.Manager;

namespace Eldergrove.Engine.Core.Contexts;

public class ActionContext
{
    public IEldergroveEngine Engine { get; set; }


    public IEldergroveEngine GetEngine() => Engine;

    public string CommandName { get; set; }
}
