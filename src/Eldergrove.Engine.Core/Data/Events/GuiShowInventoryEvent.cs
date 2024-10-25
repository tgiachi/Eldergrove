using Eldergrove.Engine.Core.Attributes.Events;
using Eldergrove.Engine.Core.GameObject;

namespace Eldergrove.Engine.Core.Data.Events;


[EventToDispatcher("gui_player_inventory")]
public record GuiShowInventoryEvent(PlayerGameObject PlayerObject);
