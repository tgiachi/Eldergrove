using Eldergrove.Engine.Core.GameObject;
using SadConsole;
using SadConsole.Input;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;

namespace Eldergrove.Ui.Core.Controls;

public class PickUpControl : ControlsConsole
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

        _window = new Window(width, height)
        {
            Position = new Point(0, 0),
            Title = "Pick Up",
            UseMouse = true,
            UseKeyboard = true,
            IsVisible = true,
            IsEnabled = true,
        };




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

        Children.Add(_window);

        _window.Controls.Add(_leftListBox);
        _window.Controls.Add(_rightListBox);

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

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        if (keyboard.IsKeyPressed(Keys.Escape))
        {
            IsEnabled = false;
            IsVisible = false;
            UseKeyboard = false;
            UseMouse = false;
            Parent.Children.Remove(this);
            return true;
        }

        return base.ProcessKeyboard(keyboard);
    }
}
