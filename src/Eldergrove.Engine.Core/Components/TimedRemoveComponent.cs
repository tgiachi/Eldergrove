using SadConsole;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class TimedRemoveComponent : RogueLikeComponentBase<GoRogue.GameFramework.GameObject>
{
    private TimeSpan _timeToLive;


    protected TimedRemoveComponent(TimeSpan timeToLive) : base(true, false, false, false)
    {
        _timeToLive = timeToLive;
    }


    public override void Update(IScreenObject host, TimeSpan delta)
    {
        _timeToLive -= delta;

        if (_timeToLive <= TimeSpan.Zero)
        {
            Parent.CurrentMap.RemoveEntity(Parent);
        }

        base.Update(host, delta);
    }
}
