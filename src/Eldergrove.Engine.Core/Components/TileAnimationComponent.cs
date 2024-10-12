using SadConsole;
using SadRogue.Integration;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class TileAnimationComponent : RogueLikeComponentBase<RogueLikeEntity>
{
    private readonly string _startingSymbol;

    private readonly string _endSymbol;

    private const int transitionTime = 500;

    private int _currentTime = 0;

    private bool _state = false;

    public TileAnimationComponent(string startingSymbol, string endSymbol) : base(true, false, false, false)
    {
        _startingSymbol = startingSymbol;
        _endSymbol = endSymbol;
    }

    public override void Update(IScreenObject host, TimeSpan delta)
    {
        _currentTime += delta.Milliseconds;

        if (_currentTime >= transitionTime)
        {
            var glyph = _state ? _startingSymbol[0] : _endSymbol[0];
            Parent.AppearanceSingle.Appearance.GlyphCharacter = glyph;

            _state = !_state;

            _currentTime = 0;
        }

        base.Update(host, delta);
    }
}
