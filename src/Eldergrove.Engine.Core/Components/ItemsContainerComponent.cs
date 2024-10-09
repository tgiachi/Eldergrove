using Eldergrove.Engine.Core.GameObject;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class ItemsContainerComponent : RogueLikeComponentBase<PropGameObject>
{
    public List<ItemGameObject> Items { get; set; }

    public ItemsContainerComponent(List<ItemGameObject> items) : base(false, false, false, false)
    {
        Items = items;
    }
}
