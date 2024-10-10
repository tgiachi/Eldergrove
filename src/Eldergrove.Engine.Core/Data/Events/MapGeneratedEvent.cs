using Eldergrove.Engine.Core.Attributes.Events;
using Eldergrove.Engine.Core.Maps;

namespace Eldergrove.Engine.Core.Data.Events;


[EventToDispatcher("map_generated")]
public record MapGeneratedEvent(GameMap Map);
