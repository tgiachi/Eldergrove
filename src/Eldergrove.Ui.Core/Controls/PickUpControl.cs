using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Ui.Core.Controls.Base;
using SadConsole;
using SadConsole.Input;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;

namespace Eldergrove.Ui.Core.Controls;

public class PickUpControl : BaseGuiControl
{
    private readonly PlayerGameObject _playerGameObject;

    private readonly List<ItemGameObject> _items;

    private readonly ListBox _leftListBox;

    private readonly ListBox _rightListBox;

    private readonly Window _window;

    public PickUpControl(int width, int height, PlayerGameObject playerGameObject, List<ItemGameObject> items) : base(
        width,
        height
    )
    {
        _playerGameObject = playerGameObject;
        _items = items;

        //Center of screen


        this.DrawBox(
            new Rectangle(0, 0, Width, Height),
            ShapeParameters.CreateBorder(new ColoredGlyph(Color.White, Color.Black, 205), true)
        );


        _leftListBox = new ListBox(width / 2, height)
        {
            Position = new Point(0, 0),
            UseMouse = true,
            UseKeyboard = true,
            CanFocus = true,
            IsVisible = true,
            IsEnabled = true,
        };

        _rightListBox = new ListBox(width / 2, height)
        {
            Position = new Point(width / 2, 0),
            UseMouse = true,
            UseKeyboard = true,
            CanFocus = true,
            IsVisible = true,
            IsEnabled = true,
        };


        Controls.Add(_leftListBox);
        Controls.Add(_rightListBox);


        ToCenter();
        PopulateListBoxes();
    }

    private void PopulateListBoxes()
    {
        _leftListBox.Items.Add("Test");
        _rightListBox.Items.Add("Test");
        _rightListBox.Items.Add("Test");
        _rightListBox.Items.Add("Test");
        _rightListBox.Items.Add("Test");
        _rightListBox.Items.Add("Test");
    }
}
