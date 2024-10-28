using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.State;
using Eldergrove.Ui.Core.Controls.Base;
using Eldergrove.Ui.Core.Data.Internal;
using SadConsole;
using SadRogue.Primitives;
using Serilog;

namespace Eldergrove.Ui.Core.Controls;

public class InventoryPanel : BaseGuiControl
{
    private readonly PlayerGameObject _playerGameObject;

    private IconListBox<InventoryListItem> _iconListBox;


    public InventoryPanel(Point size, PlayerGameObject playerGameObject) : base(size, title: "Inventory")
    {
        FocusOnShowEnabled = true;
        CenterOnShowEnabled = true;

        _playerGameObject = playerGameObject;

        _iconListBox = new IconListBox<InventoryListItem>(FullWithoutBorderSize, EldergroveState.DefaultUiFont);

        _iconListBox.Position = new Point(1, 1);

        Children.Add(_iconListBox);

        _iconListBox.UseKeyboard = true;


        _iconListBox.Items.Add(new InventoryListItem(0, "Item 1"));
        _iconListBox.Items.Add(new InventoryListItem(0, "Item 2"));
        _iconListBox.Items.Add(new InventoryListItem(0, "Item 3"));
        _iconListBox.Items.Add(new InventoryListItem(0, "Item 4"));

        _iconListBox.SelectedIndexChanged += item => { Log.Information($"Selected item: {item.Text}"); };
    }
}
