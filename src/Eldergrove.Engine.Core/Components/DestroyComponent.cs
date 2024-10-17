using Eldergrove.Engine.Core.GameObject;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class DestroyComponent : RogueLikeComponentBase<PropGameObject>
{
    public List<ItemGameObject> Items { get; set; }

    public DestroyComponent(List<ItemGameObject> items) : base(false, false, false, false)
    {
        Items = items;


        Parent.Destroyed += ParentOnDestroyed;
    }

    private void ParentOnDestroyed(object? sender, object e)
    {
    }
}
