using Eldergrove.Engine.Core.Attributes.Events;
using Eldergrove.Engine.Core.GameObject;

namespace Eldergrove.Engine.Core.Data.Events;

[EventToDispatcher("gui_pick_up_request")]
public record GuiPickUpRequestEvent(PlayerGameObject Player, List<ItemGameObject> Items);
