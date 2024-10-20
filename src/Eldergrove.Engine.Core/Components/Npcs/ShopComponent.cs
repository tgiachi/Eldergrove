using Eldergrove.Engine.Core.GameObject;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components.Npcs;

public class ShopComponent : RogueLikeComponentBase<NpcGameObject>
{
    public List<ItemGameObject> Items { get; set; } = new();

    public ShopComponent(List<ItemGameObject> items) : base(false, false, false, false)
    {
        Items = items;
    }
}
