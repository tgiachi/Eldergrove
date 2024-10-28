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

    private TextControl _descriptionTextControl;


    public InventoryPanel(Point size, PlayerGameObject playerGameObject) : base(size, title: "Inventory")
    {
        FocusOnShowEnabled = true;
        CenterOnShowEnabled = true;

        _playerGameObject = playerGameObject;


        _iconListBox = new IconListBox<InventoryListItem>(new Point(Width - 20, Height), EldergroveState.DefaultUiFont);
        _descriptionTextControl = new TextControl(new Point(Width - _iconListBox.Width - 1, Height));
        _descriptionTextControl.Background = Color.Blue;

        _iconListBox.Position = new Point(1, 1);
        _descriptionTextControl.Position = new Point(_iconListBox.Width + 1, 1);

        Children.Add(_iconListBox);
        Children.Add(_descriptionTextControl);

        _iconListBox.UseKeyboard = true;
        //        _descriptionTextControl.FontSize = new Point(2, 2);


        _iconListBox.Items.Add(new InventoryListItem(0, "Item 1", new ItemGameObject(Point.None, new ColoredGlyph())));
        _iconListBox.Items.Add(new InventoryListItem(0, "Item 2", new ItemGameObject(Point.None, new ColoredGlyph())));
        _iconListBox.Items.Add(new InventoryListItem(0, "Item 3", new ItemGameObject(Point.None, new ColoredGlyph())));

        _iconListBox.SelectedIndexChanged += item =>
        {
            Log.Information($"Selected item: {item.Text}");
            _descriptionTextControl.Text = "Description: " + item.Text;
        };
    }
}
