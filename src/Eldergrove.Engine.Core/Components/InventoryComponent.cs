using Eldergrove.Engine.Core.GameObject;
using GoRogue.GameFramework;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class InventoryComponent : RogueLikeComponentBase<IGameObject>
{
    public List<ItemGameObject> Items { get; set; }

    public InventoryComponent(List<ItemGameObject> items) : base(false, false, false, false)
    {
        Items = items;
    }
}
