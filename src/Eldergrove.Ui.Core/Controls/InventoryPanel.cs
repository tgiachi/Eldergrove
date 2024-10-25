using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Ui.Core.Controls.Base;
using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Ui.Core.Controls;

public class InventoryPanel : BaseGuiControl
{
    private readonly PlayerGameObject _playerGameObject;

    public InventoryPanel(Point size, PlayerGameObject playerGameObject) : base(size.X, size.Y)
    {
        _playerGameObject = playerGameObject;
        this.FillWithRandomGarbage(Font);

        ToCenter();
    }
}
