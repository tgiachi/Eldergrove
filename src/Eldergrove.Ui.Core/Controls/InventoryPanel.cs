using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Ui.Core.Controls.Base;
using SadRogue.Primitives;

namespace Eldergrove.Ui.Core.Controls;

public class InventoryPanel : BaseGuiControl
{
    private readonly PlayerGameObject _playerGameObject;


    public InventoryPanel(Point size, PlayerGameObject playerGameObject) : base(size, title: "Inventory")
    {
        FocusOnShowEnabled = true;
        CenterOnShowEnabled = true;

        _playerGameObject = playerGameObject;


    }
}
