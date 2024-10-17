using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using SadRogue.Integration.Components;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Components;

public class DestroyComponent : RogueLikeComponentBase<PropGameObject>
{
    public List<ItemGameObject> Items { get; set; }

    public DestroyComponent(List<ItemGameObject> items) : base(false, false, false, false)
    {
        Items = items;


       Added += ParentOnAdded;
    }

    private void ParentOnAdded(object? sender, EventArgs e)
    {
        Parent.Destroyed += ParentOnDestroyed;
    }

    private void ParentOnDestroyed(object? sender, object e)
    {
        var radiusPos = Radius.Circle.PositionsInRadius(Parent.Position, 2);


      radiusPos.RandomElements()

    }
}
