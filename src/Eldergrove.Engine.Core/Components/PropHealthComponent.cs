using Eldergrove.Engine.Core.GameObject;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class PropHealthComponent : RogueLikeComponentBase<PropGameObject>
{
    public int Health { get; set; }

    public PropHealthComponent(int initialHealth) : base(false, false, false, false)
    {
        Health = initialHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }

        if (!Parent.CanDestroy)
        {
            return;
        }

        Health -= damage;

        if (Health <= 0)
        {
            Parent.Destroy();
        }
    }
}
