using Eldergrove.Engine.Core.GameObject;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class SkillsComponent : RogueLikeComponentBase<NpcGameObject>
{
    public int Health { get; set; }

    public SkillsComponent() : base(false, false, false, false)
    {
    }
}
