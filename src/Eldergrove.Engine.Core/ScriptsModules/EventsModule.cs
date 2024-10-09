using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Services;
using NLua;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class EventsModule
{
    private readonly IEventDispatcherService _eventDispatcherService;

    public EventsModule(IEventDispatcherService eventDispatcherService)
    {
        _eventDispatcherService = eventDispatcherService;
    }


    [ScriptFunction("dispatch_event")]
    public void DispatchEvent(string eventName, object? eventData = null)
    {
        _eventDispatcherService.DispatchEvent(eventName, eventData);
    }

    [ScriptFunction("sub_event")]
    public void SubscribeToEvent(string eventName, Action<object?> eventHandler)
    {
        _eventDispatcherService.SubscribeToEvent(eventName, eventHandler);
    }

    [ScriptFunction("on_ready")]
    public void SubscribeOnReady(LuaFunction eventHandler)
    {
        _eventDispatcherService.SubscribeToEvent("engine_ready", (data) => eventHandler.Call(data));
    }
}
